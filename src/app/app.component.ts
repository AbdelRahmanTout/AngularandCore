import { ShowProductImagesDialogComponent } from './show-product-images-dialog/show-product-images-dialog.component';
import { DialogComponent } from './dialog/dialog.component';
import { ApiService } from './services/api.service';
import { MatDialog } from '@angular/material/dialog';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'dragAndAdopAngular';
  constructor(private dialog: MatDialog,private api : ApiService){

  }

  openDialog(){
    this.dialog.open(ShowProductImagesDialogComponent,{

    });
  }

  getAllProducts(){
    this.api.getProduct().subscribe({
      next:(res)=>{
        console.log(res);
      },error:(err)=>{
        alert("error while fetching");
      }
    })
  }

  ngOnInit(): void{
    this.getAllProducts();
  }
}
