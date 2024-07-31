using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

namespace DogBoard.Utils
{
    public class GetAPI : MonoBehaviour
    {
        private static Material mat;
        private static string imgUrl;
        private static GameObject ilgo;

        //Initializing the Code
        public void Initialize(Material material)
        {
            //setting the screen material to be refrenced 
            mat = material;
            FetchApi();

        }

        public void doApi()
        {
            FetchApi();
        }

        public static async Task FetchApi()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    //Uhhhhhhhhhhh I think it sends a request to the api
                    HttpResponseMessage response = await client.GetAsync("https://dog.ceo/api/breeds/image/random");
                    // Make sure that it be chillin
                    response.EnsureSuccessStatusCode();
                    //the response from the api 😱
                    string responseBody = await response.Content.ReadAsStringAsync();

                    //Parses the json response EX: {"message":"https:\/\/images.dog.ceo\/breeds\/pekinese\/n02086079_4456.jpg","status":"success"} -->> https://images.dog.ceo//breeds//pekinese//n02086079_4456.jpg 
                    string imgLink  = ParseMessageLink(responseBody);

                    //Debug Message so I know it works
                    Debug.Log(imgLink);
                    imgUrl = imgLink;

                    LoadImage(imgUrl, mat);
                    
                }
                catch (Exception e)
                {
                    Debug.Log("Exception Caught");
                    Debug.Log("Message : " + e.Message);
                }
            }

            

        }

        //Ummmm there is probably a better way of doing this 🥰🥰🥰🥰
        public static void LoadImage(string url, Material material)
        {
            if (ilgo == null)
            {
                ilgo = new GameObject("DogImageLoader");
            }
            GetAPI getAPI = ilgo.AddComponent<GetAPI>();
            getAPI.StartCoroutine(getAPI.GetImage(url, material));
        }

        //Load Image
        public IEnumerator GetImage(string url, Material mate)
        {
            //Getting the image from the url 😘😘😘
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                //Tells the error 😱😱
                Debug.LogError(request.error);
            }
            else
            {
                // Downloads the img 🤓🤓🤓
                Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
                //Makes the img not repeat
                texture.wrapMode = TextureWrapMode.Clamp;

                //Changes the img on the screen Shader
                mate.SetTexture("_Texture", texture);
            }
        }

        //Sepeterates the Api Response
        static string ParseMessageLink(string jsonResponse)
        {
            var response = JsonConvert.DeserializeObject<ApiResponse>(jsonResponse);
            return response.Message;
        }

    }

    
    public class ApiResponse
    {
        public string Message { get; set; }
        public string Status { get; set; }
    }

}
