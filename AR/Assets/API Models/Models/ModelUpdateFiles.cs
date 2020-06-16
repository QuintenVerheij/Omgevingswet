using System;
using Newtonsoft.Json;

public class ModelUpdateFiles: IAPIModel {

    public byte[] preview;
    public byte[] model;

    public byte[] json;

    public ModelUpdateFiles(byte[] preview, byte[] model, byte[] json)
    {
        this.preview = preview;
        this.model = model;
        this.json = json;
    }

    public ModelUpdateFiles fromJson(string Json){
        return JsonConvert.DeserializeObject<ModelUpdateFiles>(Json);
    }
}