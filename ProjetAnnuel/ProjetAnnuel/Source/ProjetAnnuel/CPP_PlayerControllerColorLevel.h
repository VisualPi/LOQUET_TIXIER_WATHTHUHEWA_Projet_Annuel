// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "CPP_PawnColorLevel.h"
#include "GameFramework/PlayerController.h"
#include "CPP_PlayerControllerColorLevel.generated.h"

/**
 *
 */
UCLASS()
class PROJETANNUEL_API ACPP_PlayerControllerColorLevel: public APlayerController
{
	GENERATED_BODY()

	void BeginPlay() override;
	void BeginPlayingState() override;


	// Called to bind functionality to input
	void SetupInputComponent() override;
	void MoveRight();
	void MoveLeft();
	void MoveUp();
	void MoveDown();

protected:
	ACPP_PawnColorLevel* pawn;


};
