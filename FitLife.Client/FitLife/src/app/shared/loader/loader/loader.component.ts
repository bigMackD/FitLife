import { Component, OnInit } from '@angular/core';
import { LoaderService } from '../loader.service';

@Component({
  selector: 'app-loader',
  templateUrl: './loader.component.html',
  styleUrls: ['./loader.component.sass']
})
export class LoaderComponent implements OnInit {

  loading: boolean;

  constructor(private loaderService: LoaderService) { 
    this.loaderService.isLoading.subscribe((v) => {
      this.loading = v;
    });
  }

  ngOnInit() {
  }

}
