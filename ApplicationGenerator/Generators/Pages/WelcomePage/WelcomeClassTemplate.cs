﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 15.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace AbstraX.Generators.Pages.WelcomePage
{
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using Utils;
    using AbstraX.Generators;
    using AbstraX.Angular;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "D:\MC\RazorViewsDesigner\ApplicationGenerator\Generators\Pages\WelcomePage\WelcomeClassTemplate.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "15.0.0.0")]
    public partial class WelcomeClassTemplate : AbstraX.Generators.Base.TemplateBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public override string TransformText()
        {
            
            #line 17 "D:\MC\RazorViewsDesigner\ApplicationGenerator\Generators\Pages\WelcomePage\WelcomeClassTemplate.tt"
 
    var pageName = this.PageName.ToCamelCase();
    var className = this.PageName + "Page";
    var component = new Page(className, this.UILoadKind);

    this.Declarations.Add(component);
    this.Exports.Add(component);

    foreach (var import in this.Imports)
    {
        this.WriteLine(import.DeclarationCode);
    }

            
            #line default
            #line hidden
            this.Write("\r\n@IonicPage()\r\n@Component({\r\n  selector: \'page-");
            
            #line 33 "D:\MC\RazorViewsDesigner\ApplicationGenerator\Generators\Pages\WelcomePage\WelcomeClassTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(pageName));
            
            #line default
            #line hidden
            this.Write("\',\r\n  templateUrl: \'");
            
            #line 34 "D:\MC\RazorViewsDesigner\ApplicationGenerator\Generators\Pages\WelcomePage\WelcomeClassTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(pageName));
            
            #line default
            #line hidden
            this.Write(".html\'\r\n})\r\nexport class ");
            
            #line 36 "D:\MC\RazorViewsDesigner\ApplicationGenerator\Generators\Pages\WelcomePage\WelcomeClassTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(className));
            
            #line default
            #line hidden
            this.Write(" {\r\n\r\n  constructor(public navCtrl: NavController, public translateService: Trans" +
                    "lateService) {\r\n  }\r\n\r\n  login() {\r\n    this.navCtrl.push(\'LoginPage\');\r\n  }\r\n\r\n" +
                    "  signup() {\r\n    this.navCtrl.push(\'SignupPage\');\r\n  }\r\n}\r\n");
            return this.GenerationEnvironment.ToString();
        }
        
        #line 1 "D:\MC\RazorViewsDesigner\ApplicationGenerator\Generators\Pages\WelcomePage\WelcomeClassTemplate.tt"

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

private global::System.Collections.Generic.List<ESModule> _ExportsField;

/// <summary>
/// Access the Exports parameter of the template.
/// </summary>
private global::System.Collections.Generic.List<ESModule> Exports
{
    get
    {
        return this._ExportsField;
    }
}

private global::System.Collections.Generic.List<IDeclarable> _DeclarationsField;

/// <summary>
/// Access the Declarations parameter of the template.
/// </summary>
private global::System.Collections.Generic.List<IDeclarable> Declarations
{
    get
    {
        return this._DeclarationsField;
    }
}

private global::AbstraX.DataAnnotations.UILoadKind _UILoadKindField;

/// <summary>
/// Access the UILoadKind parameter of the template.
/// </summary>
private global::AbstraX.DataAnnotations.UILoadKind UILoadKind
{
    get
    {
        return this._UILoadKindField;
    }
}

private string _PageNameField;

/// <summary>
/// Access the PageName parameter of the template.
/// </summary>
private string PageName
{
    get
    {
        return this._PageNameField;
    }
}

private string _AuthorizeField;

/// <summary>
/// Access the Authorize parameter of the template.
/// </summary>
private string Authorize
{
    get
    {
        return this._AuthorizeField;
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
bool ExportsValueAcquired = false;
if (this.Session.ContainsKey("Exports"))
{
    this._ExportsField = ((global::System.Collections.Generic.List<ESModule>)(this.Session["Exports"]));
    ExportsValueAcquired = true;
}
if ((ExportsValueAcquired == false))
{
    object data = global::System.Runtime.Remoting.Messaging.CallContext.LogicalGetData("Exports");
    if ((data != null))
    {
        this._ExportsField = ((global::System.Collections.Generic.List<ESModule>)(data));
    }
}
bool DeclarationsValueAcquired = false;
if (this.Session.ContainsKey("Declarations"))
{
    this._DeclarationsField = ((global::System.Collections.Generic.List<IDeclarable>)(this.Session["Declarations"]));
    DeclarationsValueAcquired = true;
}
if ((DeclarationsValueAcquired == false))
{
    object data = global::System.Runtime.Remoting.Messaging.CallContext.LogicalGetData("Declarations");
    if ((data != null))
    {
        this._DeclarationsField = ((global::System.Collections.Generic.List<IDeclarable>)(data));
    }
}
bool UILoadKindValueAcquired = false;
if (this.Session.ContainsKey("UILoadKind"))
{
    this._UILoadKindField = ((global::AbstraX.DataAnnotations.UILoadKind)(this.Session["UILoadKind"]));
    UILoadKindValueAcquired = true;
}
if ((UILoadKindValueAcquired == false))
{
    object data = global::System.Runtime.Remoting.Messaging.CallContext.LogicalGetData("UILoadKind");
    if ((data != null))
    {
        this._UILoadKindField = ((global::AbstraX.DataAnnotations.UILoadKind)(data));
    }
}
bool PageNameValueAcquired = false;
if (this.Session.ContainsKey("PageName"))
{
    this._PageNameField = ((string)(this.Session["PageName"]));
    PageNameValueAcquired = true;
}
if ((PageNameValueAcquired == false))
{
    object data = global::System.Runtime.Remoting.Messaging.CallContext.LogicalGetData("PageName");
    if ((data != null))
    {
        this._PageNameField = ((string)(data));
    }
}
bool AuthorizeValueAcquired = false;
if (this.Session.ContainsKey("Authorize"))
{
    this._AuthorizeField = ((string)(this.Session["Authorize"]));
    AuthorizeValueAcquired = true;
}
if ((AuthorizeValueAcquired == false))
{
    object data = global::System.Runtime.Remoting.Messaging.CallContext.LogicalGetData("Authorize");
    if ((data != null))
    {
        this._AuthorizeField = ((string)(data));
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