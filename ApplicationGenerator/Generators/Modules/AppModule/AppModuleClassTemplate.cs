﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 15.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace AbstraX.Generators.Modules.AppModule
{
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using Utils;
    using AbstraX.Generators;
    using AbstraX.Angular;
    using AbstraX;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "D:\MC\RazorViewsDesigner\ApplicationGenerator\Generators\Modules\AppModule\AppModuleClassTemplate.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "15.0.0.0")]
    public partial class AppModuleClassTemplate : AbstraX.Generators.Base.TemplateBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public override string TransformText()
        {
            this.Write(@"import { HttpClient, HttpClientModule } from '@angular/common/http';
import { ErrorHandler, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { InAppBrowser } from '@ionic-native/in-app-browser/ngx';
import { IonicErrorHandler, IonicModule } from 'ionic-angular';
import { SplashScreen } from '@ionic-native/splash-screen';
import { StatusBar } from '@ionic-native/status-bar';
import { ");
            
            #line 23 "D:\MC\RazorViewsDesigner\ApplicationGenerator\Generators\Modules\AppModule\AppModuleClassTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.AppName));
            
            #line default
            #line hidden
            this.Write("AppComponent } from \'./app.component\';\r\nimport { ");
            
            #line 24 "D:\MC\RazorViewsDesigner\ApplicationGenerator\Generators\Modules\AppModule\AppModuleClassTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.AppName));
            
            #line default
            #line hidden
            this.Write(@"AppRoutingModule } from './app-routing.module';
import { CoreModule } from './core/core.module';
import { WINDOW_PROVIDERS } from './core/services/window.service';
import { Api } from '../providers/api/api';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { ServiceWorkerModule } from '@angular/service-worker';
import { environment } from '../environments/environment';
");
            
            #line 32 "D:\MC\RazorViewsDesigner\ApplicationGenerator\Generators\Modules\AppModule\AppModuleClassTemplate.tt"
 
    var className = this.ModuleName;
    var builder = new StringBuilder();
    var indent4 = this.GetIndent(4);

    foreach (var import in this.Imports)
    {
        this.WriteLine(import.DeclarationCode);
    }

    // TODO - add module specific imports below this block

            
            #line default
            #line hidden
            this.Write(@"import { Storage, IonicStorageModule } from '@ionic/storage';
import { LoginModule } from '../pages/login/login/login.module';
import { LoginPage } from '../pages/login/login/login';
import { Settings } from '../providers/settings/settings';

export function createTranslateLoader(http: HttpClient) {
  return new TranslateHttpLoader(http, './assets/i18n/', '.json');
}

export function provideSettings(storage: Storage) {
  return new Settings(storage, {});
}

@NgModule({
  declarations: [
    ");
            
            #line 59 "D:\MC\RazorViewsDesigner\ApplicationGenerator\Generators\Modules\AppModule\AppModuleClassTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.AppName));
            
            #line default
            #line hidden
            this.Write(@"AppComponent
  ],
  imports: [
    BrowserModule,
	AppRoutingModule,
    HttpClientModule,
    LoginModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: (createTranslateLoader),
        deps: [HttpClient]
      }
    }),
    IonicModule.forRoot(),
    IonicStorageModule.forRoot(),
    CoreModule.forRoot(),
    ServiceWorkerModule.register('ngsw-worker.js', {
      enabled: environment.production
    })
");
            
            #line 79 "D:\MC\RazorViewsDesigner\ApplicationGenerator\Generators\Modules\AppModule\AppModuleClassTemplate.tt"
  
    builder = new StringBuilder();

    if (this.AngularModule.Imports.Count > 0)
    {
        foreach (var import in this.AngularModule.Imports)
        {
            builder.AppendWithLeadingIfLength(",\r\n", indent4 + import.Name);
        }

        this.WriteLine(builder.ToString());
    }

            
            #line default
            #line hidden
            this.Write("  ],\r\n  bootstrap: [");
            
            #line 92 "D:\MC\RazorViewsDesigner\ApplicationGenerator\Generators\Modules\AppModule\AppModuleClassTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.AppName));
            
            #line default
            #line hidden
            this.Write("AppComponent],\r\n  providers: [\r\n");
            
            #line 94 "D:\MC\RazorViewsDesigner\ApplicationGenerator\Generators\Modules\AppModule\AppModuleClassTemplate.tt"

    builder = new StringBuilder();

    foreach (var provider in this.AngularModule.Providers)
    {
        builder.AppendWithLeadingIfLength(",\r\n", indent4 + provider.Name);
    }

    this.WriteLine(builder.ToString() + ",");

            
            #line default
            #line hidden
            this.Write(@"    StatusBar,
    SplashScreen,
    InAppBrowser
    Api,
    WINDOW_PROVIDERS,
    { provide: Settings, useFactory: provideSettings, deps: [Storage] },
    // Keep this to enable Ionic's runtime error handling during development
    { provide: ErrorHandler, useClass: IonicErrorHandler }  ]
})
export class ");
            
            #line 113 "D:\MC\RazorViewsDesigner\ApplicationGenerator\Generators\Modules\AppModule\AppModuleClassTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(className));
            
            #line default
            #line hidden
            this.Write(" {\r\n  public static GetApp() {\r\n    return ");
            
            #line 115 "D:\MC\RazorViewsDesigner\ApplicationGenerator\Generators\Modules\AppModule\AppModuleClassTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.AppName));
            
            #line default
            #line hidden
            this.Write("AppComponent;\r\n  }\r\n}\r\n");
            return this.GenerationEnvironment.ToString();
        }
        
        #line 1 "D:\MC\RazorViewsDesigner\ApplicationGenerator\Generators\Modules\AppModule\AppModuleClassTemplate.tt"

private global::System.EventHandler _DebugCallbackField;

/// <summary>
/// Access the DebugCallback parameter of the template.
/// </summary>
private global::System.EventHandler DebugCallback
{
    get
    {
        return this._DebugCallbackField;
    }
}

private global::System.Collections.Generic.IEnumerable<ModuleImportDeclaration> _ImportsField;

/// <summary>
/// Access the Imports parameter of the template.
/// </summary>
private global::System.Collections.Generic.IEnumerable<ModuleImportDeclaration> Imports
{
    get
    {
        return this._ImportsField;
    }
}

private global::AbstraX.Angular.AngularModule _AngularModuleField;

/// <summary>
/// Access the AngularModule parameter of the template.
/// </summary>
private global::AbstraX.Angular.AngularModule AngularModule
{
    get
    {
        return this._AngularModuleField;
    }
}

private string _AppNameField;

/// <summary>
/// Access the AppName parameter of the template.
/// </summary>
private string AppName
{
    get
    {
        return this._AppNameField;
    }
}

private string _ModuleNameField;

/// <summary>
/// Access the ModuleName parameter of the template.
/// </summary>
private string ModuleName
{
    get
    {
        return this._ModuleNameField;
    }
}


/// <summary>
/// Initialize the template
/// </summary>
public override void Initialize()
{
    base.Initialize();
    if ((this.Errors.HasErrors == false))
    {
bool DebugCallbackValueAcquired = false;
if (this.Session.ContainsKey("DebugCallback"))
{
    this._DebugCallbackField = ((global::System.EventHandler)(this.Session["DebugCallback"]));
    DebugCallbackValueAcquired = true;
}
if ((DebugCallbackValueAcquired == false))
{
    object data = global::System.Runtime.Remoting.Messaging.CallContext.LogicalGetData("DebugCallback");
    if ((data != null))
    {
        this._DebugCallbackField = ((global::System.EventHandler)(data));
    }
}
bool ImportsValueAcquired = false;
if (this.Session.ContainsKey("Imports"))
{
    this._ImportsField = ((global::System.Collections.Generic.IEnumerable<ModuleImportDeclaration>)(this.Session["Imports"]));
    ImportsValueAcquired = true;
}
if ((ImportsValueAcquired == false))
{
    object data = global::System.Runtime.Remoting.Messaging.CallContext.LogicalGetData("Imports");
    if ((data != null))
    {
        this._ImportsField = ((global::System.Collections.Generic.IEnumerable<ModuleImportDeclaration>)(data));
    }
}
bool AngularModuleValueAcquired = false;
if (this.Session.ContainsKey("AngularModule"))
{
    this._AngularModuleField = ((global::AbstraX.Angular.AngularModule)(this.Session["AngularModule"]));
    AngularModuleValueAcquired = true;
}
if ((AngularModuleValueAcquired == false))
{
    object data = global::System.Runtime.Remoting.Messaging.CallContext.LogicalGetData("AngularModule");
    if ((data != null))
    {
        this._AngularModuleField = ((global::AbstraX.Angular.AngularModule)(data));
    }
}
bool AppNameValueAcquired = false;
if (this.Session.ContainsKey("AppName"))
{
    this._AppNameField = ((string)(this.Session["AppName"]));
    AppNameValueAcquired = true;
}
if ((AppNameValueAcquired == false))
{
    object data = global::System.Runtime.Remoting.Messaging.CallContext.LogicalGetData("AppName");
    if ((data != null))
    {
        this._AppNameField = ((string)(data));
    }
}
bool ModuleNameValueAcquired = false;
if (this.Session.ContainsKey("ModuleName"))
{
    this._ModuleNameField = ((string)(this.Session["ModuleName"]));
    ModuleNameValueAcquired = true;
}
if ((ModuleNameValueAcquired == false))
{
    object data = global::System.Runtime.Remoting.Messaging.CallContext.LogicalGetData("ModuleName");
    if ((data != null))
    {
        this._ModuleNameField = ((string)(data));
    }
}


    }
}


        
        #line default
        #line hidden
    }
    
    #line default
    #line hidden
}