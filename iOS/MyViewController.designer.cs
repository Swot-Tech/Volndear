// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace Volndear.iOS
{
    [Register ("MyViewController")]
    partial class MyViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView VwContainer { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (VwContainer != null) {
                VwContainer.Dispose ();
                VwContainer = null;
            }
        }
    }
}