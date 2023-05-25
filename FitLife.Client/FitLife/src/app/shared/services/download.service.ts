import { HttpClient } from "@angular/common/http";
import { ConfigurationService } from "./configuration.service";
import { Injectable } from "@angular/core";

@Injectable({
    providedIn: 'root'
  })
  
export class DownloadService {

    constructor(private httpClient: HttpClient, private configurationService: ConfigurationService) { }
  
    public download(request: string): void{
    const url = this.configurationService.settings.apiUrl + '/Report/calories/' + request;
    this.httpClient.get(url, { responseType: 'blob' }).subscribe((response: Blob) => {
      const downloadLink = document.createElement('a');
      const blobUrl = URL.createObjectURL(response);
      downloadLink.href = blobUrl;
      downloadLink.download = 'DietReport.xlsx';
      downloadLink.click();
      URL.revokeObjectURL(blobUrl);
      document.body.removeChild(downloadLink);
    });
    }
  }