using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Proyecto26;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private string baseURI = "http://gld62bmcast.pythonanywhere.com/";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("Save Boxes")]
    public void SaveBoxes()
    {
        print("Saving boxes on the server");
        GameObject[] cubes = GameObject.FindGameObjectsWithTag("Box");
        List<Box> myBoxes = new List<Box>();

        foreach(GameObject cube in cubes)
        {
            Box myBox = new Box();
            myBox.positionX = cube.transform.position.x;
            myBox.positionY = cube.transform.position.y;
            myBox.positionZ = cube.transform.position.z;

            myBoxes.Add(myBox);
        }

        string jsonString = JsonConvert.SerializeObject(myBoxes);

        RestClient.Post(baseURI + "api/saveboxes", jsonString).Then(response =>
          {
              print(response.StatusCode.ToString() + " " + response.Text);
          })
        .Catch(err =>
        {
            var error = err as RequestException;
            print(error.StatusCode);
            print(error.Response);
            print(error.Message);
        });
    }
}
