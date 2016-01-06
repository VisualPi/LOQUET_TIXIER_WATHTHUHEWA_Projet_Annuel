// Fill out your copyright notice in the Description page of Project Settings.

#include "ProjetAnnuel.h"
#include "CPP_CaseColorLevel.h"
#include "CPP_PawnColorLevel.h"
#include <string>

// Sets default values
ACPP_CaseColorLevel::ACPP_CaseColorLevel()
{
	// Set this actor to call Tick() every frame.  You can turn this off to improve performance if you don't need it.
	PrimaryActorTick.bCanEverTick = true;
	initMaterials();

	static ConstructorHelpers::FObjectFinder<UStaticMesh> Mesh1(TEXT("StaticMesh'/Engine/EngineMeshes/Cube.Cube'"));
	BaseMesh = Mesh1.Object;
	BaseStaticMesh = CreateDefaultSubobject<UStaticMeshComponent>(TEXT("Base Mesh"));
	BaseStaticMesh->SetStaticMesh(BaseMesh);
	BaseStaticMesh->SetCollisionProfileName("BlockAllDynamic");
	BaseStaticMesh->AttachTo(RootComponent);
	BaseStaticMesh->SetRelativeScale3D(FVector(5.f, 5.f, 0.5f));



	static ConstructorHelpers::FObjectFinder<UStaticMesh> Mesh2(TEXT("StaticMesh'/Game/Meshes/internCircle.internCircle'"));
	InternCircleMesh = Mesh2.Object;
	InternCircleStaticMesh = CreateDefaultSubobject<UStaticMeshComponent>(TEXT("Intern Circle Mesh"));
	InternCircleStaticMesh->SetStaticMesh(InternCircleMesh);
	//InternCircleStaticMesh->SetCollisionProfileName("BlockAllDynamic");
	InternCircleStaticMesh->AttachTo(RootComponent);



	static ConstructorHelpers::FObjectFinder<UStaticMesh> Mesh3(TEXT("StaticMesh'/Game/Meshes/circle.circle'"));
	ExternCircleMesh = Mesh3.Object;
	ExternCircleStaticMesh = CreateDefaultSubobject<UStaticMeshComponent>(TEXT("Extern Circle Mesh"));
	ExternCircleStaticMesh->SetStaticMesh(ExternCircleMesh);
	//ExternCircleStaticMesh->SetCollisionProfileName("BlockAllDynamic");
	ExternCircleStaticMesh->AttachTo(RootComponent);

	InternCircleStaticMesh->SetRelativeScale3D(FVector(1, 1, 1));
	InternCircleStaticMesh->SetRelativeLocation(FVector(0.f, 0.f, 150.f));
	ExternCircleStaticMesh->SetRelativeScale3D(FVector(1, 1, 1.0));
	ExternCircleStaticMesh->SetRelativeLocation(FVector(0.f, 0.f, 150.f));

	static ConstructorHelpers::FObjectFinder<UMaterial> baseMaterial(TEXT("Material'/Game/Materials/White.White'"));
	if (baseMaterial.Object != NULL)
		BaseStaticMesh->SetMaterial(0, baseMaterial.Object);

	static ConstructorHelpers::FObjectFinder<UMaterial> internCircleMaterial(TEXT("Material'/Game/Materials/White.White'"));
	if (internCircleMaterial.Object != NULL)
		InternCircleStaticMesh->SetMaterial(0, internCircleMaterial.Object);

	static ConstructorHelpers::FObjectFinder<UMaterial> externCircleMaterial(TEXT("Material'/Game/Materials/Yellow.Yellow'"));
	if (externCircleMaterial.Object != NULL)
		ExternCircleStaticMesh->SetMaterial(0, externCircleMaterial.Object);

	//BaseStaticMesh->OnComponentHit.AddDynamic(this, &ACPP_CaseColorLevel::OnHit);

}

// Called when the game starts or when spawned
void ACPP_CaseColorLevel::BeginPlay()
{
	Super::BeginPlay();

	InternCircleStaticMesh->SetRelativeScale3D(FVector(1, 1, 1));
	InternCircleStaticMesh->SetRelativeLocation(FVector(0.f, 0.f, 150.f));
	ExternCircleStaticMesh->SetRelativeScale3D(FVector(1, 1, 1.0));
	ExternCircleStaticMesh->SetRelativeLocation(FVector(0.f, 0.f, 150.f));
	BaseStaticMesh->OnComponentHit.AddDynamic(this, &ACPP_CaseColorLevel::OnHit);
}

// Called every frame
void ACPP_CaseColorLevel::Tick(float DeltaTime)
{
	Super::Tick(DeltaTime);
}

void ACPP_CaseColorLevel::OnHit(class AActor* OtherActor, class UPrimitiveComponent* OtherComp, FVector NormalImpulse, const FHitResult& Hit)
{
	//GEngine->AddOnScreenDebugMessage(-1, 5.f, FColor::Red, TEXT("COLLIDING !"));
	//GEngine->AddOnScreenDebugMessage(-1, 5.f, FColor::Red, FString::Printf(TEXT("Collide with %s !!"), *OtherActor->GetName()));
	if (OtherActor->IsA(ACPP_PawnColorLevel::StaticClass()))
	{
		ACPP_PawnColorLevel* pawn = Cast<ACPP_PawnColorLevel>(OtherActor);
		ChangeType(pawn->GetColor());
	}

}

void ACPP_CaseColorLevel::ChangeType(EType type)
{
	switch (type)
	{
	case EType::VE_Depart:
		ChangeMat(Materials->purple);
		break;
	case EType::VE_Fin:
		ChangeMat(Materials->black);
		break;
	case EType::VE_Blue:
		ChangeMat(Materials->blue);
		break;
	case EType::VE_Green:
		ChangeMat(Materials->green);
		break;
	case EType::VE_Red:
		ChangeMat(Materials->red);
		break;
	case EType::VE_Yellow:
		ChangeMat(Materials->yellow);
		break;
	default:
		ChangeMat(Materials->white);
		GEngine->AddOnScreenDebugMessage(-1, 5.f, FColor::Red, TEXT("Type not found !"));
	}	
}

void ACPP_CaseColorLevel::ChangeMat(UMaterialInterface* material)
{
	InternCircleStaticMesh->SetMaterial(0, material);
}

void ACPP_CaseColorLevel::ChangeMat2()
{
	InternCircleStaticMesh->SetMaterial(0, CurrentMaterial);
}

void ACPP_CaseColorLevel::initMaterials()
{
	Materials = new s_mat();
	static ConstructorHelpers::FObjectFinder<UMaterialInterface> mat_purple(TEXT("Material'/Game/Materials/Purple.Purple'"));
	Materials->purple = mat_purple.Object;
	static ConstructorHelpers::FObjectFinder<UMaterialInterface> mat_black(TEXT("Material'/Game/Materials/Black.Black'"));
	Materials->black = mat_black.Object;
	static ConstructorHelpers::FObjectFinder<UMaterialInterface> mat_blue(TEXT("Material'/Game/Materials/Blue.Blue'"));
	Materials->blue = mat_blue.Object;
	static ConstructorHelpers::FObjectFinder<UMaterialInterface> mat_green(TEXT("Material'/Game/Materials/Green.Green'"));
	Materials->green = mat_green.Object;
	static ConstructorHelpers::FObjectFinder<UMaterialInterface> mat_red(TEXT("Material'/Game/Materials/Red.Red'"));
	Materials->red = mat_red.Object;
	static ConstructorHelpers::FObjectFinder<UMaterialInterface> mat_yellow(TEXT("Material'/Game/Materials/Yellow.Yellow'"));
	Materials->yellow = mat_yellow.Object;
	static ConstructorHelpers::FObjectFinder<UMaterialInterface> mat_white(TEXT("Material'/Game/Materials/Yellow.Yellow'"));
	Materials->white = mat_white.Object;
}