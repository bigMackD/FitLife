import {
    Observable,
    Subject,
} from 'rxjs';
import { HttpBackend, HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { tap } from 'rxjs/operators';
import { AppSettings } from '../interfaces/appSettings.interface';



@Injectable({
    providedIn: 'root',
})
export class ConfigurationService {
    settings: AppSettings;
    private settings$ = new Subject<AppSettings>();
    private httpClient: HttpClient;
    constructor(handler: HttpBackend) {
        this.httpClient = new HttpClient(handler);
    }

    load(): Promise<AppSettings> {
        return this.httpClient
            .get<AppSettings>(`./../../../assets/config/app-settings.json`)
            .pipe(
                tap((response: AppSettings) => {
                    this.settings = response;
                    this.settings$.next(response);
                })
            ).toPromise();
    }

    getSettings$(): Observable<AppSettings> {
        return this.settings$;
    }
}
