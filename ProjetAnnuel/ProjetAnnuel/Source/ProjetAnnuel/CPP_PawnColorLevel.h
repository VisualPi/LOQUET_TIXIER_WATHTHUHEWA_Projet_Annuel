// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "GameFramework/Pawn.h"
#include "CPP_PawnColorLevel.generated.h"

UENUM(BlueprintType)		//"BlueprintType" is essential to include
enum class EType
{
        VE_Depart 	UMETA(DisplayName="DEPART"),
        VE_Fin 		UMETA(DisplayName="FIN"),
		VE_Blue		UMETA(DisplayName="BLUE"),
		VE_Green	UMETA(DisplayName="GREEN"),
		VE_Red		UMETA(DisplayName="RED"),
		VE_Yellow	UMETA(DisplayName="YELLOW")
};

UCLASS(Blueprintable)
class PROJETANNUEL_API ACPP_PawnColorLevel : public APawn
{
	GENERATED_BODY()

public:
	// Sets default values for this pawn's properties
	ACPP_PawnColorLevel();

	// Called when the game starts or when spawned
	virtual void BeginPlay() override;
	
	// Called every frame
	virtual void Tick( float DeltaSeconds ) override;

	// Called to bind functionality to input
	virtual void SetupPlayerInputComponent(class UInputComponent* InputComponent) override;

	UFUNCTION(BlueprintCallable, Category = "Color")
	EType GetColor() {return type;}

	void ACPP_PawnColorLevel::MoveRight();
	void ACPP_PawnColorLevel::MoveLeft();
	void ACPP_PawnColorLevel::MoveUp();
	void ACPP_PawnColorLevel::MoveDown();

protected:
	//UPROPERTY(EditAnywhere, BlueprintReadWrite, Category="Mesh")
	UPROPERTY(EditAnywhere)
	UStaticMeshComponent* ObjectMesh;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "Default Mesh")
	UStaticMesh* TileMesh;

	UPROPERTY(EditAnywhere)
	TEnumAsByte<EType> type;

	//USpringArmComponent* OurCameraSpringArm;
	UPROPERTY(EditAnywhere)
	UCameraComponent* OurCamera;

private:
	FVector currentPosition;
};
