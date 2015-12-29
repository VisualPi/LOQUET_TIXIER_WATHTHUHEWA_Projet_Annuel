// Fill out your copyright notice in the Description page of Project Settings.

#include "ProjetAnnuel.h"
#include "CPP_PawnColorLevel.h"


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
	//AutoPossessPlayer = EAutoReceiveInput::Player0;


}

// Called when the game starts or when spawned
void ACPP_PawnColorLevel::BeginPlay()
{
	Super::BeginPlay();
	currentPosition = FVector(-4110.f, -4110.f, 500.f);
	this->SetActorLocation(currentPosition, false, nullptr, ETeleportType::TeleportPhysics);
	this->SetActorScale3D(FVector(1, 1, 2));
	//UE_LOG(LogTemp, Error, TEXT("BEGIN PLAY!!"));
	
}

// Called every frame
void ACPP_PawnColorLevel::Tick(float DeltaTime)
{
	Super::Tick(DeltaTime);
	currentPosition = this->GetActorLocation();

}

// Called to bind functionality to input
void ACPP_PawnColorLevel::SetupPlayerInputComponent(class UInputComponent* InputComponent)
{
	Super::SetupPlayerInputComponent(InputComponent);

	InputComponent->BindAction("moveRight", IE_Pressed, this, &ACPP_PawnColorLevel::MoveRight);
	InputComponent->BindAction("moveLeft", IE_Pressed, this, &ACPP_PawnColorLevel::MoveLeft);
	InputComponent->BindAction("moveUp", IE_Pressed, this, &ACPP_PawnColorLevel::MoveUp);
	InputComponent->BindAction("moveDown", IE_Pressed, this, &ACPP_PawnColorLevel::MoveDown);

}

void ACPP_PawnColorLevel::MoveRight()
{
	currentPosition = FVector(currentPosition.X + 1280.f, currentPosition.Y, currentPosition.Z);// + 20);
	this->SetActorLocation(currentPosition, false, nullptr, ETeleportType::TeleportPhysics);
}
void ACPP_PawnColorLevel::MoveLeft()
{
	currentPosition = FVector(currentPosition.X - 1280.f, currentPosition.Y, currentPosition.Z);// + 20);
	this->SetActorLocation(currentPosition, false, nullptr, ETeleportType::TeleportPhysics);
}
void ACPP_PawnColorLevel::MoveUp()
{
	currentPosition = FVector(currentPosition.X, currentPosition.Y - 1280.f, currentPosition.Z);// + 20);
	this->SetActorLocation(currentPosition, false, nullptr, ETeleportType::TeleportPhysics);
}
void ACPP_PawnColorLevel::MoveDown()
{
	currentPosition = FVector(currentPosition.X, currentPosition.Y + 1280.f, currentPosition.Z);// + 20);
	this->SetActorLocation(currentPosition, false, nullptr, ETeleportType::TeleportPhysics);
}