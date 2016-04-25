using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;
using System.Xml;
using System.Text;

public class DownloadFile : MonoBehaviour
{
    private Slider downloadProgressBar;
    private string url = "http://www.dustinh.5gbfree.com/saves/eightiesHero.eah";

    void Start()
    {
        downloadProgressBar = GameObject.Find("Download Progress Bar").GetComponent<Slider>();
        downloadProgressBar.value = 0f;
    }

    public void StartGame()
    {
        Application.LoadLevel("home");
    }

    public void StartDownload()
    {
        StartCoroutine(FileDownload());
    }

    public void StartUpLoad()
    {
        StartCoroutine(FileUpload());
    }

    public IEnumerator FileDownload()
    {
        WWW client = new WWW(url);
        while (!client.isDone)
        {
            Debug.Log("downloaded " + (client.progress * 100).ToString() + "%...");
            downloadProgressBar.value = client.progress;
            yield return 0;
        }
        downloadProgressBar.value = 1f;
        string fullPath = Application.persistentDataPath + "/eightiesHero.eah";
        File.WriteAllBytes(fullPath, client.bytes);
        Debug.Log("downloaded " + (client.progress * 100).ToString() + "%...");

        if (client.error == null)
        {
            Debug.Log("download successful");
        }
        else
        {
            Debug.Log("download unsuccessful");
        }
    }
    
    IEnumerator FileUpload()
    {
        WWW localFile = new WWW("file:///" + Application.persistentDataPath + "/eightiesHero.eah");
        //while (!localFile.isDone)
        //{
        //    print("downloaded " + (localFile.progress * 100).ToString() + "%...");
        //    yield return 0;
        //}
        yield return localFile;
        if(localFile.error == null)
        {
            print("downloaded " + (localFile.progress * 100).ToString() + "%...");
            print("load successful");
        }
        else
        {
            Debug.Log("Open file error: " + localFile.error);
            yield break; // stop the coroutine here
        }

        string fileName = Application.persistentDataPath + "/eightiesHero.eah";

        WWWForm form = new WWWForm();

        form.AddBinaryData("theFile", localFile.bytes, fileName, "text/x-generic");

        //change the url to the url of the php file
        WWW upload = new WWW("http://www.dustinh.5gbfree.com/php/uploader.php", form);           

        yield return upload;

        if (upload.error == null)
            Debug.Log("upload done :" + upload.text);
        else
            Debug.Log("Error during upload: " + upload.error);
        
        if(upload.error == null)
        {
            //this part validates the upload, by waiting 5 seconds then trying to retrieve it from the web
            if (upload.uploadProgress == 1 && upload.isDone)
            {
                yield return new WaitForSeconds(5);
                //change the url to the url of the folder you want it the levels to be stored, the one you specified in the php file
                WWW upload2 = new WWW("http://www.dustinh.5gbfree.com/saves/" + fileName);
                yield return upload2;
                if (upload2.error != null)
                {
                    Debug.Log(upload2.error);
                }
            }
        }
    }
}
/*
//making a dummy xml level file
            XmlDocument map = new XmlDocument();
            map.LoadXml("<level></level>");

            //converting the xml to bytes to be ready for upload
            byte[] levelData = Encoding.UTF8.GetBytes(map.OuterXml);

            //generate a long random file name , to avoid duplicates and overwriting
            string fileName = Path.GetRandomFileName();
            fileName = fileName.Substring(0, 6);
            fileName = fileName.ToUpper();
            fileName = fileName + ".xml";

            //if you save the generated name, you can make people be able to retrieve the uploaded file, without the needs of listings
            //just provide the level code name , and it will retrieve it just like a qrcode or something like that, please read below the method used to validate the upload,
            //that same method is used to retrieve the just uploaded file, and validate it
            //this method is similar to the one used by the popular game bike baron
            //this method saves you from the hassle of making complex server side back ends which enlists available levels
            //this way you could enlist outstanding levels just by posting the levels code on a blog or forum, this way its easier to share, without the need of user accounts or install procedures
            WWWForm form = new WWWForm();

            print("form created ");

            form.AddField("action", "level upload");

            form.AddField("file", "file");

            form.AddBinaryData("file", levelData, fileName, "text/xml");

            print("binary data added ");
            //change the url to the url of the php file
            WWW w = new WWW("http://www.Upload.php", form);
            print("www created");

            yield return w;
            print("after yield w");
            if (w.error != null)
            {
                print("error");
                print(w.error);
            }
            else
            {
                //this part validates the upload, by waiting 5 seconds then trying to retrieve it from the web
                if (w.uploadProgress == 1 && w.isDone)
            {
                    yield return new WaitForSeconds(5);
                    //change the url to the url of the folder you want it the levels to be stored, the one you specified in the php file
                    WWW w2 = new WWW("http://www.eightiesactionhero.x10host.com/" + fileName);
                    yield return w2;
                    if (w2.error != null)
                    {
                        print("error 2");
                        print(w2.error);
                    }
                    else
                    {
                        //then if the retrieval was successful, validate its content to ensure the level file integrity is intact
                        if (w2.text != null  && w2.text != "")
                    {
                            if (w2.text.Contains("<level>") && w2.text.Contains("</level>"))
                        {
                                //and finally announce that everything went well
                                print("Level File " + fileName + " Contents are: \n\n" + w2.text);
                                print("Finished Uploading Level " + fileName);
                            }
                        else
                        {
                                print("Level File " + fileName + " is Invalid");
                            }
                        }
                    else
                    {
                            print("Level File " + fileName + " is Empty");
                        }
                    }
                }
            }
*/
