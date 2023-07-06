#include "[WIDGET_NAME].h"
#include "Blueprint/UserWidget.h"
#include "Blueprint/WidgetTree.h"
[INCLUDES_SECTION]

U[WIDGET_NAME]::U[WIDGET_NAME]() {
    // Nothing to do
}

U[WIDGET_NAME]::~U[WIDGET_NAME]() {
    // Nothing to do
}

void U[WIDGET_NAME]::Attach(UUserWidget* rootWidget) {
    _rootWidget = rootWidget;
    
[FIND_WIDGETS_SECTION]
}
