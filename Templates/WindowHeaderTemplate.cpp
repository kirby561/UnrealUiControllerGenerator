#include "[WIDGET_NAME].h"
#include "Blueprint/UserWidget.h"
#include "Blueprint/WidgetTree.h"
[INCLUDES_SECTION]

U[WIDGET_NAME]::U[WIDGET_NAME]() {
    static ConstructorHelpers::FClassFinder<UUserWidget> widgetTemplate(TEXT("[WIDGET_CONTENT_PATH]"));
    _widgetTemplate = widgetTemplate.Class;
}

U[WIDGET_NAME]::~U[WIDGET_NAME]() {
	// Nothing to do
}
    
void U[WIDGET_NAME]::AddToViewport(APlayerController* playerController) {
	_rootWidget = Cast<UUserWidget>(CreateWidget(playerController, _widgetTemplate));
	
[FIND_WIDGETS_SECTION]

	_rootWidget->AddToViewport();
}

void U[WIDGET_NAME]::RemoveFromViewport() {
	_rootWidget->RemoveFromParent();
}
