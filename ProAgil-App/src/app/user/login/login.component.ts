import { AuthService } from './../../_services/auth.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  titulo = 'Login';
  model: any = {};

  constructor(private toastr: ToastrService, private authService: AuthService, public route: Router) { }

  ngOnInit() {
    if (localStorage.getItem('token') != null) {
      this.route.navigate(['/dashboard']);
    }
  }

  login() {
    this.authService.login(this.model)
      .subscribe(
        () => {
          this.route.navigate(['/dashboard']);
          this.toastr.success('Login com sucesso!!');
        },
        error => {
          this.toastr.error('Falha ao logar');
        }
      );
  }
  registration() {
    this.route.navigate(['/user/registration']);
  }

}
