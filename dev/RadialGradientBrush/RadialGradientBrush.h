// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

#pragma once

#include "pch.h"
#include "common.h"

#include "RadialGradientBrush.g.h"
#include "RadialGradientBrush.properties.h"

/*  TODO:
    1.  Is it worth versioning to implement Windows.UI.Composition.IAnimationObject on 1809+ in order to better support animation?
        We could also revisit once IAnimationObject is available downlevel in WinUI 3.
    2.  Is it worth using D2D to provide a fallback gradient downlevel? Device resources are expensive.
*/

class RadialGradientBrush :
    public ReferenceTracker<RadialGradientBrush, winrt::implementation::RadialGradientBrushT>,
    public RadialGradientBrushProperties
{

public:
    RadialGradientBrush();
    ~RadialGradientBrush() {};

    winrt::IObservableVector<winrt::GradientStop> GradientStops();

    // IXamlCompositionBrushBase overrides
    void OnConnected();
    void OnDisconnected();

    void OnPropertyChanged(const winrt::DependencyPropertyChangedEventArgs& args);

private:
    bool m_isConnected{};
    winrt::CompositionBrush m_brush{ nullptr };
    winrt::IObservableVector<winrt::GradientStop> m_gradientStops{ nullptr };

    winrt::event_token m_gradientStopsChangedToken{};
    winrt::event_token m_fallbackColorChangedToken{};

    void OnFallbackColorChanged(const winrt::DependencyObject& sender, const winrt::DependencyProperty& args);

    void EnsureCompositionBrush();
    void UpdateCompositionGradientEllipseCenter();
    void UpdateCompositionGradientEllipseRadius();
    void UpdateCompositionGradientOriginOffset();
    void UpdateCompositionGradientStops();
    void UpdateCompositionGradientMappingMode();

    void UpdateFallbackBrush();
};
