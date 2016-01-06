// Fill out your copyright notice in the Description page of Project Settings.

#pragma once
#include "CPP_PawnColorLevel.h"
#include "CPP_CaseColorLevel.h"
#include <vector>
#include "Engine/LevelScriptActor.h"
#include "CPP_ColorLevelScriptActor.generated.h"

/**
 * 
 */
UCLASS()
class PROJETANNUEL_API ACPP_ColorLevelScriptActor : public ALevelScriptActor
{
	GENERATED_BODY()


	void BeginPlay() override;
	void Tick(float DeltaTime) override;

	bool CheckMovement(const FVector pos, ACPP_PawnColorLevel* pawn);
	

private:
	std::vector<ACPP_PawnColorLevel*> pawns;
	std::vector<ACPP_CaseColorLevel*> cases;
	AActor* camera;
	float timeEllapsed;
};
