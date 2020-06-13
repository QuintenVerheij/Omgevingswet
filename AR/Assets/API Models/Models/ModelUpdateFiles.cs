using System;
using Newtonsoft.Json;

public class ModelUpdateFiles: IAPIModel {

    public byte[] preview;
    public byte[] model;

    public ModelUpdateFiles(byte[] preview, byte[] model)
    {
        this.preview = preview;
        this.model = model;
    }

    public ModelUpdateFiles fromJson(string Json){
        return JsonConvert.DeserializeObject<ModelUpdateFiles>(Json);
    }
}