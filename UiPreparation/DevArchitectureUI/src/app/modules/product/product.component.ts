import { Component, OnInit, ElementRef } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { AuthService } from 'app/core/components/admin/login/Services/Auth.service';
import { LookUp } from 'app/core/models/LookUp';
import { AlertifyService } from 'app/core/services/Alertify.service';
import { LookUpService } from 'app/core/services/LookUp.service';
import { IDropdownSettings } from 'ng-multiselect-dropdown';
import { Product } from './Models/Product';
import { ProductListDto } from './Models/ProductListDto';
import { ProductService } from './Services/Product.service';
import { environment } from '../../../environments/environment';





declare var jQuery: any;

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.scss']
})
export class ProductComponent implements OnInit {

  productListDto: ProductListDto[];
  product: Product = new Product;

  categorylookUp: LookUp[];
  dropdownSettings: IDropdownSettings;
  selectedItem: number;


  constructor(private productService: ProductService, private alertifyService: AlertifyService, private lookUpService: LookUpService, private formBuilder: FormBuilder, private authService:AuthService) { }

  productAddForm: FormGroup;

  ngOnInit() {

    var drpSetting = environment.getDropDownSetting;
    drpSetting.singleSelection = true;
    this.dropdownSettings = drpSetting;

    jQuery("#categorySelect").selectpicker();

    this.createGroupAddForm();

    this.getProductList();

    this.lookUpService.getCategoryLookUp().subscribe(data => {
      this.categorylookUp = data
    });


  }

  getProductList() {
    this.productService.getProductList().subscribe(data => {
      this.productListDto = data
    });
  }

  getProductById(id: number) {
    this.clearFormGroup(this.productAddForm);

    this.productService.getProduct(id).subscribe(data => {
      this.product = data;
      this.productAddForm.patchValue(data);
    })
  }

  createGroupAddForm() {

    this.productAddForm = this.formBuilder.group({
      productId: [0],
      categoryId: ["", Validators.required],
      productName: ["", Validators.required]
    })
  }

  save() {

    if (this.productAddForm.valid) {
      this.product = Object.assign({}, this.productAddForm.value)

      if (this.product.productId == 0)
        this.addProduct();
      else
        this.updateProduct();
    }
  }

  addProduct() {

    this.productService.addProduct(this.product).subscribe(data => {
      this.getProductList();
      this.product = new Product();
      jQuery("#product").modal("hide");
      this.alertifyService.success(data);
      this.clearFormGroup(this.productAddForm);

    })

  }

  updateProduct() {

    this.productService.updateProduct(this.product).subscribe(data => {

      this.getProductList();
      this.product = new Product();
      jQuery("#group").modal("hide");
      this.alertifyService.success(data);
      this.clearFormGroup(this.productAddForm);

    })

  }

  clearFormGroup(group: FormGroup) {

    group.markAsUntouched();
    group.reset();

    Object.keys(group.controls).forEach(key => {
      group.get(key).setErrors(null);
      if (key == "productId")
        group.get(key).setValue(0);
    });
  }

  checkClaim(claim:string):boolean{
    return this.authService.claimGuard(claim)
  }


}
