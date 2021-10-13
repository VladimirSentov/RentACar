import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ImageService } from 'src/app/services/image.service';

@Component({
  selector: 'app-image-add',
  templateUrl: './image-add.component.html',
  styleUrls: ['./image-add.component.css']
})
export class ImageAddComponent implements OnInit {
  
  imageAddForm:FormGroup;

  constructor(
    private imageService:ImageService,
    private toastrService:ToastrService,
    private formBuilder:FormBuilder) { }

    ngOnInit(): void {
      this.createImageAddForm();
    }
    createImageAddForm(){
      this.imageAddForm=this.formBuilder.group({
        imageName:["",Validators.required]
      })
    }

    // addImage(){
    //   if(this.imageAddForm.valid){
    //     let imageModel = Object.assign({},this.imageAddForm.value);
    //     this.imageService.addImage(imageModel).subscribe(
    //       response => {
    //       this.toastrService.success(response.message,"Successful")
    //       },
    //       responseError => {
    //       if(responseError.error.ValidationErrors.length > 0) {
    //         for(let i=0;i<responseError.error.ValidationErrors.length;i++) {
    //           this.toastrService.error(responseError.error.ValidationErrors[i].ErrorMessage,"Validation Error")
    //         }
    //       }
    //     })
    //   }
    //   else {
    //     this.toastrService.error("The form is missing.","Attention!")
    //   }
   // }
  
  }







