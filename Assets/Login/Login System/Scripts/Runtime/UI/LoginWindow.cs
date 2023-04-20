﻿using DevionGames.UIWidgets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DevionGames.LoginSystem
{
    public class LoginWindow : UIWidget
    {
        public override string[] Callbacks
        {
            get
            {
                List<string> callbacks = new List<string>(base.Callbacks);
                callbacks.Add("OnLogin");
                callbacks.Add("OnFailedToLogin");
                return callbacks.ToArray();
            }
        }

        [Header("Reference")]
        /// <summary>
		/// Referenced UI field
		/// </summary>
		[SerializeField]
        protected InputField username;
        /// <summary>
        /// Referenced UI field
        /// </summary>
        [SerializeField]
        protected InputField password;
        /// <summary>
        /// Referenced UI field
        /// </summary>
        [SerializeField]
        protected Toggle rememberMe;
        /// <summary>
        /// Referenced UI field
        /// </summary>
        [SerializeField]
        protected Button loginButton;
        [SerializeField]
        protected GameObject loadingIndicator;

        protected override void OnStart()
        {
            base.OnStart();
            username.text = PlayerPrefs.GetString("username", string.Empty);
            password.text = PlayerPrefs.GetString("password", string.Empty);

            if (rememberMe != null)
            {
                rememberMe.isOn = string.IsNullOrEmpty(username.text) ? false : true;
            }
          
            if (loadingIndicator != null){
                loadingIndicator.SetActive(false);
            }

            EventHandler.Register("OnLogin", OnLogin);
            EventHandler.Register("OnFailedToLogin", OnFailedToLogin);

            loginButton.onClick.AddListener(LoginUsingFields);
        }

        public void LoginUsingFields() {
            UserManager.LoginAccount(username.text, password.text);
            loginButton.interactable = false;
            if (loadingIndicator != null) {
                loadingIndicator.SetActive(true);
            }
        }

        private void OnLogin() {
     
        }

        private void OnFailedToLogin() {
            Execute("OnFailedToLogin", new CallbackEventData());
            username.text = "";
            password.text = "";
           
            loginButton.interactable = true;
            if (loadingIndicator != null)
            {
                loadingIndicator.SetActive(false);
            }
            Close();
        }
    }
}