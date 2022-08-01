import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { LookUp } from '../../../models/LookUp';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { Company } from './models/Company';
import { CompanyService } from './services/Company.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-company',
	templateUrl: './company.component.html',
	styleUrls: ['./company.component.scss']
})
export class CompanyComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	companylookUp: LookUp[];
	displayedColumns: string[] = ['id','tenantId','name','firmName','address','phone','phone2','email','taxNo','webSite', 'update','delete'];

	companyList:Company[];
	company:Company=new Company();

	companyAddForm: FormGroup;


	tenantId:number;

	constructor(private companyService:CompanyService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getCompanyList();
    }

	ngOnInit() {
		// this.lookupService.getCompanyLookup().subscribe(data => {
		// 	this.companylookUp = data;
		// })

		this.createCompanyAddForm();
	}


	getCompanyList() {
		this.companyService.getCompanyList().subscribe(data => {
			this.companyList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.companyAddForm.valid) {
			this.company = Object.assign({}, this.companyAddForm.value)

			if (this.company.id == 0)
				this.addCompany();
			else
				this.updateCompany();
		}

	}

	addCompany(){

		this.companyService.addCompany(this.company).subscribe(data => {
			this.getCompanyList();
			this.company = new Company();
			jQuery('#company').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.companyAddForm);

		})

	}

	updateCompany(){

		this.companyService.updateCompany(this.company).subscribe(data => {

			var index=this.companyList.findIndex(x=>x.id==this.company.id);
			this.companyList[index]=this.company;
			this.dataSource = new MatTableDataSource(this.companyList);
            this.configDataTable();
			this.company = new Company();
			jQuery('#company').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.companyAddForm);

		})

	}

	createCompanyAddForm() {
		this.companyAddForm = this.formBuilder.group({		
			id : [0],
//tenantId : [0],
name : ["", Validators.required],
firmName : ["", Validators.required],
address : ["", Validators.required],
phone : ["", Validators.required],
phone2 : ["", Validators.required],
email : ["", Validators.required],
taxNo : ["", Validators.required],
webSite : ["", Validators.required]
		})
	}

	deleteCompany(companyId:number){
		this.companyService.deleteCompany(companyId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.companyList=this.companyList.filter(x=> x.id!=companyId);
			this.dataSource = new MatTableDataSource(this.companyList);
			this.configDataTable();
		})
	}

	getCompanyById(companyId:number){
		this.clearFormGroup(this.companyAddForm);
		this.companyService.getCompanyById(companyId).subscribe(data=>{
			this.company=data;
			this.companyAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'id')
				group.get(key).setValue(0);
		});
	}

	checkClaim(claim:string):boolean{
		return this.authService.claimGuard(claim)
	}

	configDataTable(): void {
		this.dataSource.paginator = this.paginator;
		this.dataSource.sort = this.sort;
	}

	applyFilter(event: Event) {
		const filterValue = (event.target as HTMLInputElement).value;
		this.dataSource.filter = filterValue.trim().toLowerCase();

		if (this.dataSource.paginator) {
			this.dataSource.paginator.firstPage();
		}
	}

  }
