import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavbarComponent } from "./pageComponent/navbar/navbar.component";
import { ReactiveFormsModule } from '@angular/forms';
import { FooterComponent } from "./pageComponent/footer/footer.component"; 

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, NavbarComponent, ReactiveFormsModule, FooterComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'ProjetoIntegradorAngular';
}
