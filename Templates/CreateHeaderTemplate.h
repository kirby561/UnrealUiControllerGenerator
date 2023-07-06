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
    
    UUserWidget* Create(APlayerController* playerController);
    
private: // Properties
    UPROPERTY()
    UClass* _widgetTemplate = nullptr;
    
    UPROPERTY()
    UUserWidget* _rootWidget = nullptr;
[PROPERTY_DEFINITION_SECTION]
};
