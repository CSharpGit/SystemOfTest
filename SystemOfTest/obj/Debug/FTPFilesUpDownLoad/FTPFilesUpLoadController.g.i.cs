﻿#pragma checksum "..\..\..\FTPFilesUpDownLoad\FTPFilesUpLoadController.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1B94B23FC52B50C3F9F89183BC89DB115D3983FF"
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


namespace SystemOfTest.FTPFilesUpDownLoad {
    
    
    /// <summary>
    /// FTPFilesUpLoadController
    /// </summary>
    public partial class FTPFilesUpLoadController : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 7 "..\..\..\FTPFilesUpDownLoad\FTPFilesUpLoadController.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid gdUpLoad;
        
        #line default
        #line hidden
        
        
        #line 8 "..\..\..\FTPFilesUpDownLoad\FTPFilesUpLoadController.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_uplaod;
        
        #line default
        #line hidden
        
        
        #line 10 "..\..\..\FTPFilesUpDownLoad\FTPFilesUpLoadController.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnUpload;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\..\FTPFilesUpDownLoad\FTPFilesUpLoadController.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel filePanel;
        
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
            System.Uri resourceLocater = new System.Uri("/SystemOfTest;component/ftpfilesupdownload/ftpfilesuploadcontroller.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\FTPFilesUpDownLoad\FTPFilesUpLoadController.xaml"
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
            this.gdUpLoad = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.btn_uplaod = ((System.Windows.Controls.Button)(target));
            
            #line 8 "..\..\..\FTPFilesUpDownLoad\FTPFilesUpLoadController.xaml"
            this.btn_uplaod.Click += new System.Windows.RoutedEventHandler(this.btn_uplaod_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btnUpload = ((System.Windows.Controls.Button)(target));
            
            #line 10 "..\..\..\FTPFilesUpDownLoad\FTPFilesUpLoadController.xaml"
            this.btnUpload.Click += new System.Windows.RoutedEventHandler(this.btn_UpLoad);
            
            #line default
            #line hidden
            return;
            case 4:
            this.filePanel = ((System.Windows.Controls.StackPanel)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

