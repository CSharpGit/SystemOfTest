﻿#pragma checksum "..\..\TeachersLoging.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "87B469F09F5A2FF87911C75A309A6AFDFBD226B7"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using SystemOfTest;


namespace SystemOfTest {
    
    
    /// <summary>
    /// TeachersLoging
    /// </summary>
    public partial class TeachersLoging : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 9 "..\..\TeachersLoging.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid TL_Grid;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\TeachersLoging.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label label;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\TeachersLoging.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label label1;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\TeachersLoging.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label label3;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\TeachersLoging.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ID;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\TeachersLoging.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label label4;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\TeachersLoging.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox PS;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\TeachersLoging.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btLoging;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\TeachersLoging.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btExit;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/SystemOfTest;component/teachersloging.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\TeachersLoging.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 8 "..\..\TeachersLoging.xaml"
            ((SystemOfTest.TeachersLoging)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.TL_Grid = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.label = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.label1 = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.label3 = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.ID = ((System.Windows.Controls.TextBox)(target));
            
            #line 19 "..\..\TeachersLoging.xaml"
            this.ID.KeyDown += new System.Windows.Input.KeyEventHandler(this.ID_KeyDown);
            
            #line default
            #line hidden
            return;
            case 7:
            this.label4 = ((System.Windows.Controls.Label)(target));
            return;
            case 8:
            this.PS = ((System.Windows.Controls.PasswordBox)(target));
            
            #line 23 "..\..\TeachersLoging.xaml"
            this.PS.KeyDown += new System.Windows.Input.KeyEventHandler(this.PS_KeyDown);
            
            #line default
            #line hidden
            return;
            case 9:
            this.btLoging = ((System.Windows.Controls.Button)(target));
            
            #line 26 "..\..\TeachersLoging.xaml"
            this.btLoging.Click += new System.Windows.RoutedEventHandler(this.btLoging_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.btExit = ((System.Windows.Controls.Button)(target));
            
            #line 27 "..\..\TeachersLoging.xaml"
            this.btExit.Click += new System.Windows.RoutedEventHandler(this.btExit_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

