﻿using System;
using System.Windows.Threading;

using SlightPenLighter.Models;
using SlightPenLighter.UI;

namespace SlightPenLighter.Hooks
{
    public class MouseTracker
    {
        public IntPtr WindowPointer { get; set; }

        public PenHighlighter Highlighter { get; private set; }

        public MouseTracker(IntPtr window, PenHighlighter highlighter)
        {
            WindowPointer = window;
            Highlighter = highlighter;

            var hookManager = new HookManager();
            hookManager.MouseMove += HookManagerOnMouseMove;
            hookManager.MouseClick += HookManagerOnMouseClick;

            hookManager.Start();
        }

        private void HookManagerOnMouseMove(PhysicalPoint next)
        {
            DwmHelper.MoveWindow(WindowPointer, next.X, next.Y);
        }

        private void HookManagerOnMouseClick()
        {
            Highlighter.ClickEvent = true;
            Highlighter.Dispatcher.Invoke(() => Highlighter.ClickEvent = false, DispatcherPriority.Background);
        }
    }
}