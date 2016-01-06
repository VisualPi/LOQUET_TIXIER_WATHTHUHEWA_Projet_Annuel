// Fill out your copyright notice in the Description page of Project Settings.

#include "ProjetAnnuel.h"
#include "CPP_PlayerControllerColorLevel.h"




void ACPP_PlayerControllerColorLevel::BeginPlay()
{
	Super::BeginPlay();
	
}

void ACPP_PlayerControllerColorLevel::BeginPlayingState() //evenement appeler apres la possession, si dans beginPlay -> plantage
{
	Super::BeginPlayingState();
	pawn = Cast<ACPP_PawnColorLevel, APawn>(GetControlledPawn());
}

// Called to bind functionality to input
void ACPP_PlayerControllerColorLevel::SetupInputComponent()
{
	Super::SetupInputComponent();

	int32 id = GetLocalPlayer()->GetControllerId();
	switch (id)
	{
	case 0:
		this->InputComponent->BindAction("moveRight_P0", IE_Pressed, this, &ACPP_PlayerControllerColorLevel::MoveRight);
		this->InputComponent->BindAction("moveLeft_P0", IE_Pressed, this, &ACPP_PlayerControllerColorLevel::MoveLeft);
		this->InputComponent->BindAction("moveUp_P0", IE_Pressed, this, &ACPP_PlayerControllerColorLevel::MoveUp);
		this->InputComponent->BindAction("moveDown_P0", IE_Pressed, this, &ACPP_PlayerControllerColorLevel::MoveDown);
		break;
	case 1:
		this->InputComponent->BindAction("moveRight_P1", IE_Pressed, this, &ACPP_PlayerControllerColorLevel::MoveRight);
		this->InputComponent->BindAction("moveLeft_P1", IE_Pressed, this, &ACPP_PlayerControllerColorLevel::MoveLeft);
		this->InputComponent->BindAction("moveUp_P1", IE_Pressed, this, &ACPP_PlayerControllerColorLevel::MoveUp);
		this->InputComponent->BindAction("moveDown_P1", IE_Pressed, this, &ACPP_PlayerControllerColorLevel::MoveDown);
		break;
	case 2:
		this->InputComponent->BindAction("moveRight_P2", IE_Pressed, this, &ACPP_PlayerControllerColorLevel::MoveRight);
		this->InputComponent->BindAction("moveLeft_P2", IE_Pressed, this, &ACPP_PlayerControllerColorLevel::MoveLeft);
		this->InputComponent->BindAction("moveUp_P2", IE_Pressed, this, &ACPP_PlayerControllerColorLevel::MoveUp);
		this->InputComponent->BindAction("moveDown_P2", IE_Pressed, this, &ACPP_PlayerControllerColorLevel::MoveDown);
		break;
	case 3:
		this->InputComponent->BindAction("moveRight_P3", IE_Pressed, this, &ACPP_PlayerControllerColorLevel::MoveRight);
		this->InputComponent->BindAction("moveLeft_P3", IE_Pressed, this, &ACPP_PlayerControllerColorLevel::MoveLeft);
		this->InputComponent->BindAction("moveUp_P3", IE_Pressed, this, &ACPP_PlayerControllerColorLevel::MoveUp);
		this->InputComponent->BindAction("moveDown_P3", IE_Pressed, this, &ACPP_PlayerControllerColorLevel::MoveDown);
		break;
	}


}

void ACPP_PlayerControllerColorLevel::MoveRight()
{
	if (pawn)
	{
		FVector pos = FVector(pawn->GetCurrentPos().X + 1280.f, pawn->GetCurrentPos().Y, pawn->GetCurrentPos().Z);
		//GEngine->AddOnScreenDebugMessage(-1, 50.f, FColor::Red, FString::Printf(TEXT("supposed to move to : (%f, %f, %f)"), pos.X, pos.Y, pos.Z));
		pawn->SetNextPos(pos);
	}
}
void ACPP_PlayerControllerColorLevel::MoveLeft()
{
	if (pawn)
	{
		FVector pos = FVector(pawn->GetCurrentPos().X - 1280.f, pawn->GetCurrentPos().Y, pawn->GetCurrentPos().Z);
		//GEngine->AddOnScreenDebugMessage(-1, 50.f, FColor::Red, FString::Printf(TEXT("supposed to move to : (%f, %f, %f)"), pos.X, pos.Y, pos.Z));
		pawn->SetNextPos(pos);
	}
}
void ACPP_PlayerControllerColorLevel::MoveUp()
{
	if (pawn)
	{
		FVector pos = FVector(pawn->GetCurrentPos().X, pawn->GetCurrentPos().Y - 1280.f, pawn->GetCurrentPos().Z);
		//GEngine->AddOnScreenDebugMessage(-1, 50.f, FColor::Red, FString::Printf(TEXT("supposed to move to : (%f, %f, %f)"), pos.X, pos.Y, pos.Z));
		pawn->SetNextPos(pos);
	}
}
void ACPP_PlayerControllerColorLevel::MoveDown()
{
	if (pawn)
	{
		FVector pos = FVector(pawn->GetCurrentPos().X, pawn->GetCurrentPos().Y + 1280.f, pawn->GetCurrentPos().Z);
		//GEngine->AddOnScreenDebugMessage(-1, 50.f, FColor::Red, FString::Printf(TEXT("supposed to move to : (%f, %f, %f)"), pos.X, pos.Y, pos.Z));
		pawn->SetNextPos(pos);
	}
}