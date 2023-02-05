import {
  AfterViewInit,
  Component,
  ElementRef,
  OnInit,
  ViewChild,
} from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { User } from "./models/user";
import { UserService } from "./services/user.service";
import { IDropdownSettings } from "ng-multiselect-dropdown";
import { LookUp } from "app/core/models/lookUp";
import { AlertifyService } from "app/core/services/alertify.service";
import { LookUpService } from "app/core/services/lookUp.service";
import { AuthService } from "../login/services/auth.service";
import { MustMatch } from "app/core/directives/must-match";
import { PasswordDto } from "./models/passwordDto";
import { environment } from "environments/environment";
import { MatSort } from "@angular/material/sort";
import { MatPaginator } from "@angular/material/paginator";
import { MatTableDataSource } from "@angular/material/table";

declare var jQuery: any;

@Component({
  selector: "app-user",
  templateUrl: "./user.component.html",
  styleUrls: ["./user.component.scss"],
})
export class UserComponent implements AfterViewInit, OnInit {
  dataSource: MatTableDataSource<any>;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  displayedColumns: string[] = [
    "userId",
    "email",
    "fullName",
    "status",
    "mobilePhones",
    "address",
    "notes",
    "passwordChange",
    "updateClaim",
    "updateGroupClaim",
    "update",
    "delete",
  ];

  user: User;
  userList: User[];
  groupDropdownList: LookUp[];
  groupSelectedItems: LookUp[];
  dropdownSettings: IDropdownSettings;

  claimDropdownList: LookUp[];
  claimSelectedItems: LookUp[];

  isGroupChange: boolean = false;
  isClaimChange: boolean = false;

  userId: number;

  constructor(
    private userService: UserService,
    private formBuilder: FormBuilder,
    private alertifyService: AlertifyService,
    private lookUpService: LookUpService,
    private authService: AuthService
  ) {}

  ngAfterViewInit(): void {
    this.getUserList();
  }

  userAddForm: FormGroup;
  passwordForm: FormGroup;

  ngOnInit() {
    this.createUserAddForm();
    this.createPasswordForm();

    this.dropdownSettings = environment.getDropDownSetting;

    this.lookUpService.getGroupLookUp().subscribe((data) => {
      this.groupDropdownList = data;
    });

    this.lookUpService.getOperationClaimLookUp().subscribe((data) => {
      this.claimDropdownList = data;
    });
  }

  getUserGroupPermissions(userId: number) {
    this.userId = userId;

    this.userService.getUserGroupPermissions(userId).subscribe((data) => {
      this.groupSelectedItems = data;
    });
  }

  getUserClaimsPermissions(userId: number) {
    this.userId = userId;

    this.userService.getUserClaims(userId).subscribe((data) => {
      this.claimSelectedItems = data;
    });
  }

  saveUserGroupsPermissions() {
    if (this.isGroupChange) {
      var ids = this.groupSelectedItems.map(function (x) {
        return x.id as number;
      });
      this.userService.saveUserGroupPermissions(this.userId, ids).subscribe(
        (x) => {
          jQuery("#groupPermissions").modal("hide");
          this.isGroupChange = false;
          this.alertifyService.success(x);
        },
        (error) => {
          this.alertifyService.error(error.error);
          jQuery("#groupPermissions").modal("hide");
        }
      );
    }
  }

  saveUserClaimsPermission() {
    if (this.isClaimChange) {
      var ids = this.claimSelectedItems.map(function (x) {
        return x.id as number;
      });
      this.userService.saveUserClaims(this.userId, ids).subscribe(
        (x) => {
          jQuery("#claimsPermissions").modal("hide");
          this.isClaimChange = false;
          this.alertifyService.success(x);
        },
        (error) => {
          this.alertifyService.error(error.error);
          jQuery("#claimsPermissions").modal("hide");
        }
      );
    }
  }

  onItemSelect(comboType: string) {
    this.setComboStatus(comboType);
  }

  onSelectAll(comboType: string) {
    this.setComboStatus(comboType);
  }
  onItemDeSelect(comboType: string) {
    this.setComboStatus(comboType);
  }

  setComboStatus(comboType: string) {
    if (comboType == "Group") this.isGroupChange = true;
    else if (comboType == "Claim") this.isClaimChange = true;
  }

  createUserAddForm() {
    this.userAddForm = this.formBuilder.group({
      userId: [0],
      fullName: ["", Validators.required],
      email: ["", Validators.required],
      address: [""],
      notes: [""],
      mobilePhones: [""],
      status: [true],
    });
  }

  createPasswordForm() {
    this.passwordForm = this.formBuilder.group(
      {
        password: ["", Validators.required],
        confirmPassword: ["", Validators.required],
      },
      {
        validator: MustMatch("password", "confirmPassword"),
      }
    );
  }

  getUserList() {
    this.userService.getUserList().subscribe((data) => {
      this.userList = data;
      this.dataSource = new MatTableDataSource(data);
      this.configDataTable();
    });
  }

  clearFormGroup(group: FormGroup) {
    group.markAsUntouched();
    group.reset();

    Object.keys(group.controls).forEach((key) => {
      group.get(key).setErrors(null);
      if (key == "userId") group.get(key).setValue(0);
      else if (key == "status") group.get(key).setValue(true);
    });
  }

  setUserId(id: number) {
    this.userId = id;
  }

  save() {
    if (this.userAddForm.valid) {
      this.user = Object.assign({}, this.userAddForm.value);

      if (this.user.userId == 0) this.addUser();
      else this.updateUser();
    }
  }

  savePassword() {
    if (this.passwordForm.valid) {
      var passwordDto: PasswordDto = new PasswordDto();
      passwordDto.userId = this.userId;
      passwordDto.password = this.passwordForm.value.password;

      this.userService.saveUserPassword(passwordDto).subscribe((data) => {
        this.userId = 0;
        jQuery("#passwordChange").modal("hide");
        this.alertifyService.success(data);
        this.clearFormGroup(this.passwordForm);
      });
    }
  }

  addUser() {
    this.userService.addUser(this.user).subscribe((data) => {
      this.getUserList();
      this.user = new User();
      jQuery("#user").modal("hide");
      this.alertifyService.success(data);
      this.clearFormGroup(this.userAddForm);
    });
  }

  getUserById(id: number) {
    this.clearFormGroup(this.userAddForm);
    this.userService.getUserById(id).subscribe((data) => {
      this.user = data;
      this.userAddForm.patchValue(data);
    });
  }

  updateUser() {
    this.userService.updateUser(this.user).subscribe((data) => {
      var index = this.userList.findIndex((x) => x.userId == this.user.userId);
      this.userList[index] = this.user;
      this.dataSource = new MatTableDataSource(this.userList);
      this.configDataTable();
      this.user = new User();
      jQuery("#user").modal("hide");
      this.alertifyService.success(data);
      this.clearFormGroup(this.userAddForm);
    });
  }

  deleteUser(id: number) {
    this.userService.deleteUser(id).subscribe((data) => {
      this.alertifyService.success(data.toString());
      var index = this.userList.findIndex((x) => x.userId == id);
      this.userList[index].status = false;
      this.dataSource = new MatTableDataSource(this.userList);
      this.configDataTable();
    });
  }

  checkClaim(claim: string): boolean {
    return this.authService.claimGuard(claim);
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
