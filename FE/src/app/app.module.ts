import { provideHttpClient, withInterceptorsFromDi } from "@angular/common/http";
import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { AppComponent } from "./app.component";
import { FormsModule } from "@angular/forms";

@NgModule({
    declarations: [AppComponent],
    imports: [BrowserModule, FormsModule],
    exports: [BrowserModule],
    providers: [provideHttpClient(withInterceptorsFromDi())],
    bootstrap: [AppComponent]
})
export class AppModule {}