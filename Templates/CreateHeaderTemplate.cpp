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

UUserWidget* U[WIDGET_NAME]::Create(APlayerController* playerController) {
    _rootWidget = Cast<UUserWidget>(CreateWidget(playerController, _widgetTemplate));
    
[FIND_WIDGETS_SECTION]

    return _rootWidget;
}
