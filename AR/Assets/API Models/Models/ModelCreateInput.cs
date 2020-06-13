using System;
using Newtonsoft.Json;

public class ModelCreateInput: IAPIModel {
    public int id;
    public int userid;
    public bool pub;
    public int visibleRange;
    public double longitude;
    public double latitude;
    public string createdAt;

    public ModelCreateInput(int id, int userid, bool pub, int visibleRange, double longitude, double latitude, string createdAt)
    {
        this.id = id;
        this.userid = userid;
        this.pub = pub;
        this.visibleRange = visibleRange;
        this.longitude = longitude;
        this.latitude = latitude;
        this.createdAt = createdAt;
    }

    public ModelCreateInput fromJson(string Json){
        return JsonConvert.DeserializeObject<ModelCreateInput>(Json);
    }

    public override string ToString() {
        return String.Format("id: {0}, userId: {1},public: {2},visibleRange: {3},longitude: {4}, latitude: {5}, createdAt: {6}",
        this.id,
        this.userid,
        this.pub,
        this.visibleRange,
        this.longitude,
        this.latitude,
        this.createdAt);
        
    }
}