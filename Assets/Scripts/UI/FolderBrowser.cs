using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

using TMPro;

using UnityEngine;

public class FolderBrowser : MonoBehaviour
{
    private const int WM_USER = 0x400;
    private const int BFFM_INITIALIZED = 1;
    private const int BFFM_SELCHANGED = 2;
    private const int BFFM_SETSELECTIONW = WM_USER + 103;
    private const int BFFM_SETSTATUSTEXTW = WM_USER + 104;

    // Browsing for directory
    private uint BIF_RETURNONLYFSDIRS = 0x0001;  // For finding a folder to start document searching
    private uint BIF_VALIDATE = 0x0020;   // insist on valid result (or CANCEL)
    private uint BIF_USENEWUI = 0x0040 + 0x0010; //(BIF_NEWDIALOGSTYLE | BIF_EDITBOX);
    private uint BIF_BROWSEINCLUDEURLS = 0x0080;   // Allow URLs to be displayed or entered. (Requires BIF_USENEWUI)
    private uint BIF_NONEWFOLDERBUTTON = 0x0200;   // Do not add the "New Folder" button to the dialog.  Only applicable with BIF_NEWDIALOGSTYLE.
    private uint BIF_SHAREABLE = 0x8000;  // sharable resources displayed (remote shares, requires BIF_USENEWUI)

    [DllImport("shell32.dll")] private static extern IntPtr SHBrowseForFolder(ref BROWSEINFO lpbi);
    [DllImport("shell32.dll", CharSet = CharSet.Unicode)] private static extern bool SHGetPathFromIDList(IntPtr pidl, IntPtr pszPath);
    [DllImport("user32.dll", PreserveSig = true)] private static extern IntPtr SendMessage(HandleRef hWnd, uint Msg, int wParam, IntPtr lParam);
    [DllImport("user32.dll", CharSet = CharSet.Auto)] private static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, string lParam);

    private string _initialPath;
    public delegate int BrowseCallBackProc(IntPtr hwnd, int msg, IntPtr lp, IntPtr wp);

    private struct BROWSEINFO
    {
        public IntPtr hwndOwner;
        public IntPtr pidlRoot;
        public string pszDisplayName;
        public string lpszTitle;
        public uint ulFlags;
        public BrowseCallBackProc lpfn;
        public IntPtr lParam;
        public int iImage;
    }

    private int OnBrowseEvent(IntPtr hWnd, int msg, IntPtr lp, IntPtr lpData)
    {
        switch (msg)
        {
            case BFFM_INITIALIZED: // Required to set initialPath
            {
                SendMessage(new HandleRef(null, hWnd), BFFM_SETSELECTIONW, 1, _initialPath);
                break;
            }
            case BFFM_SELCHANGED:
            {
                IntPtr pathPtr = Marshal.AllocHGlobal(260 * Marshal.SystemDefaultCharSize);
                if (SHGetPathFromIDList(lp, pathPtr)) { SendMessage(new HandleRef(null, hWnd), BFFM_SETSTATUSTEXTW, 0, pathPtr); }
                Marshal.FreeHGlobal(pathPtr);
                break;
            }
        }

        return 0;
    }

    private string SelectFolder(string caption, string initialPath, IntPtr parentHandle)
    {
        _initialPath = initialPath;
        string finalPath = "";
        IntPtr bufferAddress = Marshal.AllocHGlobal(256);
        IntPtr pidl = IntPtr.Zero;

        BROWSEINFO bi = new BROWSEINFO();
        bi.hwndOwner = parentHandle;
        bi.pidlRoot = IntPtr.Zero;
        bi.lpszTitle = caption;
        bi.ulFlags = BIF_USENEWUI | BIF_VALIDATE | BIF_SHAREABLE | BIF_BROWSEINCLUDEURLS | BIF_NONEWFOLDERBUTTON | BIF_RETURNONLYFSDIRS;
        bi.lpfn = new BrowseCallBackProc(OnBrowseEvent);
        bi.lParam = IntPtr.Zero;
        bi.iImage = 0;

        try
        {
            pidl = SHBrowseForFolder(ref bi);
            if (true != SHGetPathFromIDList(pidl, bufferAddress))
            {
                return null;
            }
            finalPath = Marshal.PtrToStringAuto(bufferAddress);
        }
        finally
        {
            Marshal.FreeCoTaskMem(pidl);
        }

        return finalPath;
    }

    private TMP_InputField pathInputField;

    private void Awake()
    {
        pathInputField = transform.GetChild(1).GetComponent<TMP_InputField>();
    }

    public void BrowseButtonClicked(string _folder)
    {
        string path;

        if (!string.IsNullOrWhiteSpace(pathInputField.text))
        {
            path = pathInputField.text;
        }
        else
        {
            path = Application.streamingAssetsPath.Replace("/", "\\");
        }

        string result = SelectFolder("Select the parent folder for your " + _folder + " folder.", path, IntPtr.Zero);

        if (!string.IsNullOrWhiteSpace(result))
        {
            pathInputField.text = result;
        }
    }

    public void OpenButtonClicked()
    {
        string path;

        if (string.IsNullOrWhiteSpace(pathInputField.text))
        {
            path = Application.persistentDataPath.Replace("/", "\\");
        }
        else
        {
            path = pathInputField.text;
        }

        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            Arguments = path,
            FileName = path,
        };

        Process.Start(startInfo);
    }
}
