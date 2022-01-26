import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss']
})
export class EventosComponent implements OnInit {

  public eventos: any = [];
  public eventosFiltrados: any = [];

  imagemAltura: number =100;
  imagemMargem: number =2;
  mostrarImagem: boolean = true;
  private _filtroLista : string = "";

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.getEventos();
  }

  public get filtroLista(): string
  {
    return this._filtroLista;
  }
  public set filtroLista(value : string)
  {
    this._filtroLista = value;
    this.eventosFiltrados = this.filtroLista ? this.filtrarEventos(this.filtroLista) : this.eventos;

  }

public filtrarEventos(filtrarPor : string): any
{
  filtrarPor = filtrarPor.toLocaleLowerCase();
  return this.eventos.filter(
    (evento: { tema: string; local: string; })=>
        evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1 ||
        evento.local.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    );
}


  public getEventos(): void
  {
    //it is recommended only to use the anonymous function if you only specify the next callback otherwise we recommend to pass an Observer
    this.http.get('https://localhost:5001/api/eventos').subscribe({
      next: (response) => {
            this.eventos = response;
            this.eventosFiltrados = this.eventos;
      },
      error: (error) => console.error(error)}

    )

    //https://rxjs.dev/deprecations/subscribe-arguments
    // this.http.get('https://localhost:5001/api/eventos').subscribe(
    //   response => this.eventos = response,
    //   error => console.log(error)
    // );
  }

  public visualiarImagem()
  {
    this.mostrarImagem = !this.mostrarImagem;
  }

}
