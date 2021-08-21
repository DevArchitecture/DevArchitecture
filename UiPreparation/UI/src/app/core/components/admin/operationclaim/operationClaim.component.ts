import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from '../login/services/auth.service';
import { OperationClaim } from './Models/OperationClaim';
import { OperationClaimService } from './Services/OperationClaim.service';


declare var jQuery: any;

@Component({
	selector: 'app-operationClaim',
	templateUrl: './operationClaim.component.html',
	styleUrls: ['./operationClaim.component.scss']
})
export class OperationClaimComponent implements AfterViewInit, OnInit {

	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['id', 'name', 'alias', 'description', 'update'];

	operationClaimList: OperationClaim[];
	operationClaim: OperationClaim = new OperationClaim();

	operationClaimAddForm: FormGroup;

	operationClaimId: number;

	constructor(private operationClaimService: OperationClaimService, private lookupService: LookUpService, private alertifyService: AlertifyService, private formBuilder: FormBuilder, private authService: AuthService) { }
	ngAfterViewInit(): void {
		this.getOperationClaimList();
	}

	ngOnInit() {
		this.createOperationClaimAddForm();
	}

	getOperationClaimList() {
		this.operationClaimService.getOperationClaimList().subscribe(data => {
			this.operationClaimList = data;
			this.dataSource = new MatTableDataSource(data);
			this.configDataTable();
		});
	}

	save() {

		if (this.operationClaimAddForm.valid) {
			this.operationClaim = Object.assign({}, this.operationClaimAddForm.value)
			this.updateOperationClaim();
		}
	}

	updateOperationClaim() {
		this.operationClaimService.updateOperationClaim(this.operationClaim).subscribe(data => {

			var index = this.operationClaimList.findIndex(x => x.id == this.operationClaim.id);
			this.operationClaimList[index] = this.operationClaim;
			this.dataSource = new MatTableDataSource(this.operationClaimList);
			this.configDataTable();
			this.operationClaim = new OperationClaim();
			jQuery('#operationclaim').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.operationClaimAddForm);
		})

	}

	createOperationClaimAddForm() {
		this.operationClaimAddForm = this.formBuilder.group({
			id: [0],
			name: [],
			alias: [""],
			description: [""]
		})
	}

	getOperationClaimById(operationClaimId: number) {
		this.clearFormGroup(this.operationClaimAddForm);
		this.operationClaimService.getOperationClaim(operationClaimId).subscribe(data => {
			this.operationClaimAddForm.patchValue(data);
			this.operationClaim = data;
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

	applyFilter(event: Event) {
		const filterValue = (event.target as HTMLInputElement).value;
		this.dataSource.filter = filterValue.trim().toLowerCase();

		if (this.dataSource.paginator) {
			this.dataSource.paginator.firstPage();
		}
	}

}
