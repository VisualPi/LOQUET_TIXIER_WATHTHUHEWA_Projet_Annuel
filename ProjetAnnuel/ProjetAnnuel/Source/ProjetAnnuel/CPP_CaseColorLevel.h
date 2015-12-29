// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "GameFramework/Actor.h"
#include "CPP_CaseColorLevel.generated.h"

enum class EType;

struct s_mat
{
	UMaterialInterface* purple;
	UMaterialInterface* black;
	UMaterialInterface* blue;
	UMaterialInterface* green;
	UMaterialInterface* red;
	UMaterialInterface* yellow;
	UMaterialInterface* white;

};

UCLASS()
class PROJETANNUEL_API ACPP_CaseColorLevel : public AActor
{
	GENERATED_BODY()
	
public:	
	// Sets default values for this actor's properties
	ACPP_CaseColorLevel();

	// Called when the game starts or when spawned
	virtual void BeginPlay() override;
	
	// Called every frame
	virtual void Tick( float DeltaSeconds ) override;
	
	UFUNCTION()
	void OnHit(class AActor* OtherActor, class UPrimitiveComponent* OtherComp, FVector NormalImpulse, const FHitResult& Hit);

	void ChangeType(EType type);
	void ChangeMat(UMaterialInterface* material);
	void ChangeMat2();
	void initMaterials();



protected:
	UPROPERTY(EditAnywhere)
	UStaticMeshComponent* BaseStaticMesh;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "Default Mesh")
	UStaticMesh* BaseMesh;

	UPROPERTY(EditAnywhere)
	UStaticMeshComponent* InternCircleStaticMesh;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "Default Mesh")
	UStaticMesh* InternCircleMesh;

	UPROPERTY(EditAnywhere)
	UStaticMeshComponent* ExternCircleStaticMesh;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "Default Mesh")
	UStaticMesh* ExternCircleMesh;

private:
	UMaterialInterface* CurrentMaterial;
	s_mat* Materials;
	
};
