import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../login/services/auth.service';
import { Translate } from './Models/Translate';
import { TranslateService } from './Services/Translate.service';
import { LookUp } from '../../../models/LookUp';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AlertifyService } from 'app/core/services/alertify.service';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';


declare var jQuery: any;

@Component({
	selector: 'app-translate',
	templateUrl: './translate.component.html',
	styleUrls: ['./translate.component.scss']
})
export class TranslateComponent implements  AfterViewInit, OnInit {

	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;

	translateList: Translate[] = [];
	translate: Translate = new Translate();

	translateAddForm: FormGroup;

	langugelookUp: LookUp[];
	displayedColumns: string[] = ['id', 'language', 'code', 'value','update','delete'];
	dataSource: MatTableDataSource<any>;


	translateId: number;


	constructor(private translateService: TranslateService, private lookupService: LookUpService, private alertifyService: AlertifyService, private formBuilder: FormBuilder, private authService: AuthService) { }

	ngAfterViewInit(): void {

		this.getTranslateList();
	}

	applyFilter(event: Event) {
		const filterValue = (event.target as HTMLInputElement).value;
		this.dataSource.filter = filterValue.trim().toLowerCase();

		if (this.dataSource.paginator) {
			this.dataSource.paginator.firstPage();
		}
	}


	ngOnInit() {
		this.lookupService.getLanguageLookup().subscribe(data => {
			this.langugelookUp = data;
		})

		this.createTranslateAddForm();
	}


	getTranslateList() {
		this.translateService.getTranslateList().subscribe(data => {
			this.translateList = data;
			this.dataSource = new MatTableDataSource(data);
			this.configDataTable();
		});
	}

	save() {

		if (this.translateAddForm.valid) {
			this.translate = Object.assign({}, this.translateAddForm.value)

			if (this.translate.id == 0)
				this.addTranslate();
			else
				this.updateTranslate();

		}

	}

	addTranslate() {

		this.translateService.addTranslate(this.translate).subscribe(data => {
			this.getTranslateList();
			this.translate = new Translate();
			jQuery('#translate').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.translateAddForm);

		})

	}

	updateTranslate() {

		this.translateService.updateTranslate(this.translate).subscribe(data => {
			var index = this.translateList.findIndex(x => x.id == this.translate.id);
			this.translateList[index].code = this.translate.code;
			this.translateList[index].value = this.translate.value;
			this.dataSource = new MatTableDataSource(this.translateList);
			this.configDataTable();
			this.translate = new Translate();
			jQuery('#translate').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.translateAddForm);

		})

	}

	createTranslateAddForm() {
		this.translateAddForm = this.formBuilder.group({
			id: [0],
			langId: [0, Validators.required],
			code: ["", Validators.required],
			value: ["", Validators.required]
		})
	}

	deleteTranslate(translateId: number) {
		this.translateService.deleteTranslate(translateId).subscribe(data => {
			this.alertifyService.success(data.toString());
			this.translateList = this.translateList.filter(x => x.id != translateId);
			this.dataSource = new MatTableDataSource(this.translateList);
			this.configDataTable();
		})
	}

	getTranslateById(translateId: number) {
		this.clearFormGroup(this.translateAddForm);
		this.translateService.getTranslate(translateId).subscribe(data => {
			this.translate = data;
			this.translateAddForm.patchValue(data);
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

	checkClaim(claim: string): boolean {
		return this.authService.claimGuard(claim)
	}

	configDataTable(): void {
		this.dataSource.paginator = this.paginator;
		this.dataSource.sort = this.sort;
	}

}
