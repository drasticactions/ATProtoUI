﻿// <copyright file="Program.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using ObjCRuntime;
using UIKit;

namespace ATProtoMAUI;

/// <summary>
/// Main Program.
/// </summary>
public class Program
{
    // This is the main entry point of the application.
    private static void Main(string[] args)
    {
        // if you want to use a different Application Delegate class from "AppDelegate"
        // you can specify it here.
        UIApplication.Main(args, null, typeof(AppDelegate));
    }
}
