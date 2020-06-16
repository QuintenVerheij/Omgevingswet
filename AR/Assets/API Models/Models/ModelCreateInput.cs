using System;
using Newtonsoft.Json;

public class ModelCreateInput: IAPIModel {
    public AuthorizationToken token;
    public int userId;
    public bool pub;
    public int visibleRange;
    public decimal longitude;
    public decimal latitude;
    public string createdAt;

    public ModelCreateInput(string token, int userid, bool pub, int visibleRange, double longitude, double latitude, string createdAt)
    {
        this.token = new AuthorizationToken(token);
        this.userId = userid;
        this.pub = pub;
        this.visibleRange = visibleRange;
        this.longitude = new Decimal(longitude);
        this.latitude = new Decimal(latitude);
        this.createdAt = createdAt;
    }

    public ModelCreateInput fromJson(string Json){
        return JsonConvert.DeserializeObject<ModelCreateInput>(Json);
    }

    public override string ToString() {
        return String.Format("id: {0}, userId: {1},public: {2},visibleRange: {3},longitude: {4}, latitude: {5}, createdAt: {6}",
        this.token,
        this.userId,
        this.pub,
        this.visibleRange,
        this.longitude,
        this.latitude,
        this.createdAt);
        
    }
}