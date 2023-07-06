#pragma once

#include "CoreMinimal.h"
#include "[WIDGET_NAME].generated.h"

class UUserWidget;
[FORWARD_DECLARATIONS_SECTION]

/**
 * Used to keep track of the references to the UI objects in WBP_[WIDGET_NAME].uasset
 */
UCLASS()
class U[WIDGET_NAME] : public UObject {
    GENERATED_BODY()
    
public:
    U[WIDGET_NAME]();
    virtual ~U[WIDGET_NAME]();
    
    void Attach(UUserWidget* rootWidget);
    
private: // Properties
    UPROPERTY()
    UUserWidget* _rootWidget = nullptr;
[PROPERTY_DEFINITION_SECTION]
};
