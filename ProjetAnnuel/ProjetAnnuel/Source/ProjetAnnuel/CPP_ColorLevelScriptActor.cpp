// Fill out your copyright notice in the Description page of Project Settings.

#include "ProjetAnnuel.h"
#include "CPP_ColorLevelScriptActor.h"



void ACPP_ColorLevelScriptActor::BeginPlay()
{
	Super::BeginPlay();
	pawns = std::vector<ACPP_PawnColorLevel*>();
	for (FConstPawnIterator pawnIT = GetWorld()->GetPawnIterator(); pawnIT; ++pawnIT)
	{
		pawns.push_back(Cast<ACPP_PawnColorLevel, APawn>(pawnIT->Get()));
	}
	camera = nullptr;
	for (FConstCameraActorIterator camIT = GetWorld()->GetAutoActivateCameraIterator(); camIT; ++camIT)
	{
		camera = camIT->Get();
	}
	int i = 0;
	for (FConstPlayerControllerIterator Iterator = GetWorld()->GetPlayerControllerIterator(); Iterator; ++Iterator)
	{
		//GEngine->AddOnScreenDebugMessage(-1, 50.f, FColor::Red, FString::Printf(TEXT("Unique ID = %d"), Iterator->Get()->getContro));
		Iterator->Get()->Possess(pawns[i]);
		if (camera != nullptr)
			Iterator->Get()->SetViewTargetWithBlend(camera);
		i++;
	}
	for (TActorIterator<ACPP_CaseColorLevel> ActorItr(GetWorld()); ActorItr; ++ActorItr)
	{
		cases.push_back(*ActorItr);
	}
	timeEllapsed = 0.f;
}

void ACPP_ColorLevelScriptActor::Tick(float DeltaTime)
{
	Super::Tick(DeltaTime);
	for (std::vector<ACPP_PawnColorLevel*>::iterator it = pawns.begin(); it != pawns.end(); ++it)
	if (( *it )->GetNextPos().X != ( *it )->GetCurrentPos().X || ( *it )->GetNextPos().Y != ( *it )->GetCurrentPos().Y)
	if (CheckMovement(( *it )->GetNextPos(), *it))
		( *it )->Move(( *it )->GetNextPos());
}

bool ACPP_ColorLevelScriptActor::CheckMovement(const FVector pos, ACPP_PawnColorLevel* pawn)
{//tester que x,y si le z est testé ça casse tout :o
	for (std::vector<ACPP_CaseColorLevel*>::iterator it = cases.begin(); it != cases.end(); ++it)
	{
		if (pos.X > ( *it )->GetActorLocation().X - ( ( 5 * ( *it )->GetActorScale().X ) / 2 ) //5 est la longueur d'un cube par default 5*5*5
			&& pos.X < ( *it )->GetActorLocation().X + ( ( 5 * ( *it )->GetActorScale().X ) / 2 )
			&& pos.Y >(*it)->GetActorLocation().Y - ( ( 5 * ( *it )->GetActorScale().Y ) / 2 )
			&& pos.Y < ( *it )->GetActorLocation().Y + ( ( 5 * ( *it )->GetActorScale().Y ) / 2 )
			)//est ce que la case existe ?
		{
			for (std::vector<ACPP_PawnColorLevel*>::iterator it2 = pawns.begin(); it2 != pawns.end(); ++it2)
				if (( *it2 )->GetName() != pawn->GetName())
					if (pawn->GetNextPos().X == ( *it2 )->GetCurrentPos().X
						&& pawn->GetNextPos().Y == ( *it2 )->GetCurrentPos().Y) //est ce qu'il y a un joueur sur la case ?
						return false;
			return true;
		}
	}
	return false;
}