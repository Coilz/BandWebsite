﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ewk.BandWebsite.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class ExceptionMessages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ExceptionMessages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Ewk.BandWebsite.Resources.ExceptionMessages", typeof(ExceptionMessages).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Something went wrong....
        /// </summary>
        public static string GenericExceptionMessage {
            get {
                return ResourceManager.GetString("GenericExceptionMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The user name or password provided is incorrect..
        /// </summary>
        public static string InvalidCredentialsErrorMessage {
            get {
                return ResourceManager.GetString("InvalidCredentialsErrorMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The current password is incorrect or the new password is invalid..
        /// </summary>
        public static string InvalidPasswordErrorMessage {
            get {
                return ResourceManager.GetString("InvalidPasswordErrorMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A user name for that e-mail address already exists. Please enter a different e-mail address..
        /// </summary>
        public static string MembershipCreateStatus_DuplicateEmail {
            get {
                return ResourceManager.GetString("MembershipCreateStatus_DuplicateEmail", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to User name already exists. Please enter a different user name..
        /// </summary>
        public static string MembershipCreateStatus_DuplicateUserName {
            get {
                return ResourceManager.GetString("MembershipCreateStatus_DuplicateUserName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The password retrieval answer provided is invalid. Please check the value and try again..
        /// </summary>
        public static string MembershipCreateStatus_InvalidAnswer {
            get {
                return ResourceManager.GetString("MembershipCreateStatus_InvalidAnswer", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The e-mail address provided is invalid. Please check the value and try again..
        /// </summary>
        public static string MembershipCreateStatus_InvalidEmail {
            get {
                return ResourceManager.GetString("MembershipCreateStatus_InvalidEmail", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The password provided is invalid. Please enter a valid password value..
        /// </summary>
        public static string MembershipCreateStatus_InvalidPassword {
            get {
                return ResourceManager.GetString("MembershipCreateStatus_InvalidPassword", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The password retrieval question provided is invalid. Please check the value and try again..
        /// </summary>
        public static string MembershipCreateStatus_InvalidQuestion {
            get {
                return ResourceManager.GetString("MembershipCreateStatus_InvalidQuestion", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The user name provided is invalid. Please check the value and try again..
        /// </summary>
        public static string MembershipCreateStatus_InvalidUserName {
            get {
                return ResourceManager.GetString("MembershipCreateStatus_InvalidUserName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator..
        /// </summary>
        public static string MembershipCreateStatus_ProviderError {
            get {
                return ResourceManager.GetString("MembershipCreateStatus_ProviderError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator..
        /// </summary>
        public static string MembershipCreateStatus_UserRejected {
            get {
                return ResourceManager.GetString("MembershipCreateStatus_UserRejected", resourceCulture);
            }
        }
    }
}