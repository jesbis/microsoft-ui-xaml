// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

// DO NOT EDIT! This file was generated by CustomTasks.DependencyPropertyCodeGen
#include "pch.h"
#include "common.h"
#include "RadioButtonsElementFactory.h"

CppWinRTActivatableClassWithDPFactory(RadioButtonsElementFactory)

GlobalDependencyProperty RadioButtonsElementFactoryProperties::s_DataTemplateProperty{ nullptr };

RadioButtonsElementFactoryProperties::RadioButtonsElementFactoryProperties()
{
    EnsureProperties();
}

void RadioButtonsElementFactoryProperties::EnsureProperties()
{
    if (!s_DataTemplateProperty)
    {
        s_DataTemplateProperty =
            InitializeDependencyProperty(
                L"DataTemplate",
                winrt::name_of<winrt::DataTemplate>(),
                winrt::name_of<winrt::RadioButtonsElementFactory>(),
                false /* isAttached */,
                ValueHelper<winrt::DataTemplate>::BoxedDefaultValue(),
                nullptr);
    }
}

void RadioButtonsElementFactoryProperties::ClearProperties()
{
    s_DataTemplateProperty = nullptr;
}

void RadioButtonsElementFactoryProperties::DataTemplate(winrt::DataTemplate const& value)
{
    static_cast<RadioButtonsElementFactory*>(this)->SetValue(s_DataTemplateProperty, ValueHelper<winrt::DataTemplate>::BoxValueIfNecessary(value));
}

winrt::DataTemplate RadioButtonsElementFactoryProperties::DataTemplate()
{
    return ValueHelper<winrt::DataTemplate>::CastOrUnbox(static_cast<RadioButtonsElementFactory*>(this)->GetValue(s_DataTemplateProperty));
}