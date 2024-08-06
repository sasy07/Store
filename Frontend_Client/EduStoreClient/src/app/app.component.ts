import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {SharedModule} from "./shared/shared.module";
import {CoreModule} from "./core/core.module";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet , SharedModule , CoreModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'EduStoreClient';
}
