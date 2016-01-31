﻿using System;
using System.Net;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace StarterKit.Utilities.Configs
{
    public class EsbConfiguration : IEsbConfiguration
    {
        protected EsbConfiguration(IConfiguration config)
		{
			_config = config;
		}
		
		private readonly IConfiguration _config;
        
		public virtual string Prefix { get; }

		private string _authScheme;
		public string AuthScheme
		{
			get
			{
				if ( _authScheme == null )
					_authScheme = _config[Prefix + ":AuthScheme"];
				return _authScheme;
			}
		}

        private string _domain;
        public string Domain
        {
            get
            {
                if (_domain == null)
                    _domain = _config[Prefix + ":Domain"];
                return _domain;
            }
        }

        private string _user;
        public string User
        {
			get
            {
                if ( _user == null )
                    _user = _config[Prefix + ":User"];
                return _user;
            }
        }

        private string _password;
        public string Password
        {
			get
            {
                if ( _password == null )
                    _password = _config[Prefix + ":Password"];
                return _password;
            }
        }

        private string _url;
        public string Url
        {
            get
            {
                if ( _url == null )
                    _url = _config[Prefix + ":Scheme"] + _config[Prefix + ":Host"] + ":" + _config[Prefix + ":Port"] + _config[Prefix + ":Path"];
                return _url;
            }
        }

        private NetworkCredential _credential;
        public NetworkCredential Credential
        {
            get 
            {
                if ( _credential == null )
                    _credential = new NetworkCredential(string.Format("{0}\\{1}", this.Domain, this.User), this.Password);
                return _credential;
            }
        }

        private string _credentialBase64;
        public string CredentialBase64
        {
            get
            {
                if ( _credentialBase64 == null )
                {
                    var bytes = Encoding.ASCII.GetBytes(string.Format("{0}\\{1}:{2}", this.Domain, this.User, this.Password));
                    _credentialBase64 = Convert.ToBase64String(bytes);
                }
                return _credentialBase64;
            }
        }
    }
}