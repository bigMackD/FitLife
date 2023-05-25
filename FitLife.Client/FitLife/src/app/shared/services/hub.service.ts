import { HubConnection, HubConnectionBuilder, LogLevel } from "@microsoft/signalr";
import { ConfigurationService } from "./configuration.service";
import { Injectable } from "@angular/core";

@Injectable({
    providedIn: 'root'
  })
  
export class HubService {
    private _connection: HubConnection;
    constructor(private configurationService: ConfigurationService) { }
  
    public create(logLevel: LogLevel): HubService {
       this._connection = new HubConnectionBuilder().withUrl(this.configurationService.settings.hubUrl).configureLogging(logLevel).build();
       return this;
    }

    public get(): HubConnection{
        return this._connection;
    }
  }