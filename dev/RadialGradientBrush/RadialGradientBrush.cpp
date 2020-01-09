// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

#include "pch.h"
#include "common.h"
#include "RadialGradientBrush.h"
#include "RuntimeProfiler.h"
#include "ResourceAccessor.h"

RadialGradientBrush::RadialGradientBrush()
{
    __RP_Marker_ClassById(RuntimeProfiler::ProfId_RadialGradientBrush);
}

void RadialGradientBrush::OnConnected()
{

}

void RadialGradientBrush::OnDisconnected()
{

}

void  RadialGradientBrush::OnPropertyChanged(const winrt::DependencyPropertyChangedEventArgs& args)
{
    winrt::IDependencyProperty property = args.Property();
    
    // TODO: Implement
}
