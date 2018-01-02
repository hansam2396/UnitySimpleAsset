﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct SignUpInform
{
    public string emailAddr;
    public string name;
    public string password;
    public string rePassword;
}

public class SignupViewController : ViewController 
{
	[SerializeField] private InputField idInput;
	[SerializeField] private InputField nameInput;
	[SerializeField] private InputField pwInput;
	[SerializeField] private InputField rePwInput;
	[SerializeField] private Button sendBtn;
	[SerializeField] private GameObject loadingObj;

	private string errTitle="";
	private string errMessage="입력하신 정보를 다시 확인해주세요.";

    private static GameObject prefab = null;
    private SignUpInform signUpInform;

    public static SignupViewController Show()
    {
        if (prefab == null)
        {
            prefab = Resources.Load("SignUpView") as GameObject;
        }

        GameObject obj = Instantiate(prefab) as GameObject;
        SignupViewController signUpView = obj.GetComponent<SignupViewController>();
        signUpView.SetSignUpView();

        return signUpView;
    }

    public void SetSignUpView()
    {
        loadingObj.SetActive(false);
        signUpInform = new SignUpInform();

        idInput.onEndEdit.AddListener(delegate { CheckIDInput(idInput); });
        nameInput.onEndEdit.AddListener(delegate { CheckNameInput(nameInput); });
        pwInput.onEndEdit.AddListener(delegate { CheckPWInput(pwInput); });
        rePwInput.onEndEdit.AddListener(delegate { CheckRePWInput(rePwInput); });
        sendBtn.onClick.AddListener(delegate { OnPressSend(); });
    }

	private void CheckIDInput(InputField input)
    {
        if (input.text.Length != 0 && !(input.text.Contains("@") && input.text.Contains(".")))
        {
			AlertViewController.Show(errTitle, errMessage );
            return;
		}
        signUpInform.emailAddr = input.text;
    }

	private void CheckNameInput(InputField input)
    {
		if(input.text.Length != 0 && (input.text.Length > 6 || input.text.Length < 2))
		{
			AlertViewController.Show(errTitle, errMessage );
            return;
		}
        signUpInform.name = input.text;
     }

	private void CheckPWInput(InputField input)
    {
		if(input.text.Length != 0 && input.text.Length < 6)
		{
            AlertViewController.Show(errTitle, errMessage );
            return; 
		}
        signUpInform.password = input.text;
    }

	private void CheckRePWInput(InputField input)
    {
		if(input.text.Length != 0 && !input.text.Equals(pwInput.text))
		{
            AlertViewController.Show(errTitle, errMessage );
            return;
		}
        signUpInform.rePassword = input.text;
    }

	private void OnPressSend()
	{
		if(idInput.text.Length == 0 || pwInput.text.Length == 0 || rePwInput.text.Length == 0 || nameInput.text.Length == 0)
		{
			string message = "빈칸을 입력해주세요";
			AlertViewController.Show(errTitle, message);
			return;
		}

        loadingObj.SetActive(true);
        Destroy(gameObject);
    }
}