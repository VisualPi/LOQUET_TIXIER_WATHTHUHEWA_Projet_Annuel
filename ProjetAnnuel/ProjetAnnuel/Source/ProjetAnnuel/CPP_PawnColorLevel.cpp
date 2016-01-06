// Fill out your copyright notice in the Description page of Project Settings.

#include "ProjetAnnuel.h"
#include "CPP_PawnColorLevel.h"

int ACPP_PawnColorLevel::nbPlayer = 0;

// Sets default values
ACPP_PawnColorLevel::ACPP_PawnColorLevel()
{
	// Set this pawn to call Tick() every frame.  You can turn this off to improve performance if you don't need it.
	PrimaryActorTick.bCanEverTick = true;
	currentPosition = FVector::ZeroVector;

	static ConstructorHelpers::FObjectFinder<UStaticMesh> MyMesh(TEXT("StaticMesh'/Engine/EngineMeshes/Cube.Cube'"));
	TileMesh = MyMesh.Object;
	//this->AddComponent("Circle", true, FTransform(), TileMesh);
	//ObjectMesh->SetStaticMesh(TileMesh);
	//Create
	ObjectMesh = CreateDefaultSubobject<UStaticMeshComponent>(TEXT("Player Mesh"));
 
	ObjectMesh->SetStaticMesh(TileMesh);
	ObjectMesh->SetSimulatePhysics(true);
	ObjectMesh->SetCollisionProfileName("Pawn");
	ObjectMesh->SetNotifyRigidBodyCollision(true);

	ObjectMesh->AttachTo(RootComponent);
	

	//Camera
	/*RootComponent = CreateDefaultSubobject<USceneComponent>(TEXT("RootComponent"))*/;
	//OurCameraSpringArm = CreateDefaultSubobject<USpringArmComponent>(TEXT("CameraSpringArm"));
	//OurCameraSpringArm->AttachTo(RootComponent);
	//OurCameraSpringArm->SetWorldLocationAndRotation(FVector(960.0f, 10050.0f, 4650.0f), FRotator(0.0f, -1.0f, -90.0f));
	//OurCameraSpringArm->TargetArmLength = 400.f;
	//OurCameraSpringArm->bEnableCameraLag = true;
	//OurCameraSpringArm->CameraLagSpeed = 3.0f;

	//OurCamera = CreateDefaultSubobject<UCameraComponent>(TEXT("GameCamera"));
	//OurCamera->AttachTo(RootComponent, USpringArmComponent::SocketName);
	//OurCamera->SetWorldLocationAndRotation(FVector(960.0f, 10050.0f, 4650.0f), FRotator(0.0f, -1.0f, -90.0f));

	//Take control of the default Player
	


}

// Called when the game starts or when spawned
void ACPP_PawnColorLevel::BeginPlay()
{
	Super::BeginPlay();
	switch (nbPlayer)
	{
	case 0:
		AutoPossessPlayer = EAutoReceiveInput::Player0;
		currentPosition = FVector(-4110.f, -4110.f, 500.f);
		nbPlayer++;
		break;
	case 1:
		AutoPossessPlayer = EAutoReceiveInput::Player0;
		currentPosition = FVector(7410.f, -4110.f, 500.f);
		nbPlayer++;
		break;
	case 2:
		AutoPossessPlayer = EAutoReceiveInput::Player0;
		currentPosition = FVector(7410.f, 7410.f, 500.f);
		nbPlayer++;
		break;
	case 3:
		AutoPossessPlayer = EAutoReceiveInput::Player0;
		currentPosition = FVector(-4110.f, 7410.f, 500.f);
		nbPlayer++;
		break;
	default:
		//GEngine->AddOnScreenDebugMessage(-1, 50.f, FColor::Red, FString::Printf(TEXT("Too many player ! nb = %d"), nbPlayer));
		break;
	}
	nextPosition = currentPosition;
	this->SetActorLocation(currentPosition, false, nullptr, ETeleportType::TeleportPhysics);
	this->SetActorScale3D(FVector(1, 1, 2));
	//UE_LOG(LogTemp, Error, TEXT("BEGIN PLAY!!"));
	
}

void ACPP_PawnColorLevel::EndPlay(const EEndPlayReason::Type EndPlayReason)
{
	Super::EndPlay(EndPlayReason);
	nbPlayer = 0;
}

// Called every frame
void ACPP_PawnColorLevel::Tick(float DeltaTime)
{
	//GEngine->AddOnScreenDebugMessage(-1, 5.f, FColor::Red, FString::Printf(TEXT("%f"), DeltaTime));
	Super::Tick(DeltaTime);
	currentPosition = this->GetActorLocation();

}

void ACPP_PawnColorLevel::Move(FVector pos)
{
	currentPosition = FVector(pos.X, pos.Y, currentPosition.Z);
	nextPosition = currentPosition;
	SetActorLocation(currentPosition, false, nullptr, ETeleportType::TeleportPhysics);
}

