import { HttpClient } from '@angular/common/http';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Evento } from '../../models/Evento';
import { EventoService } from '../../services/evento.service';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss']
})
export class EventosComponent implements OnInit {
  modalRef!: BsModalRef;
  public eventos: Evento[] = [];
  public eventosFiltrados: any = [];
  public widthImg = 180;
  public marginImg = 2;
  public showImg = true;
  private filtroListado = '';

  public get filtroLista(): string  {
    return this.filtroListado;
  }

  public set filtroLista(value: string) {
    this.filtroListado = value;
    this.eventosFiltrados = this.filtroLista ? this.filtrarEventos(this.filtroLista) : this.getEventos();
  }

  public filtrarEventos(filtrarPor: string): Evento[] {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.eventos.filter(
      (evento: any) => evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1 ||
                       evento.local.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    );
  }

  constructor( private eventoService: EventoService
             , private modalService: BsModalService
             , private toastrService: ToastrService
             , private spinner: NgxSpinnerService) { }

  public ngOnInit(): void {
    this.getEventos();
    this.spinner.show();

    setTimeout(() => {
      /** spinner ends after 5 seconds */

    }, 5000);

  }

  public showImage(): void{
    this.showImg = ! this.showImg;
  }
  public getEventos(): void {

    this.eventoService.getEventos().subscribe({
      next: (eventos: Evento[]) => {
        this.eventos = eventos;
        this.eventosFiltrados = this.eventos;
      },
      error: (error: any) =>
      {
          console.log(error);
          this.spinner.hide();
          this.toastrService.error('Erro ao Carregar os eventos', 'Erro');

      },
      complete: () =>  this.spinner.hide()
    });

  }

  openModal(template: TemplateRef<any>): void {
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  confirm(): void {
    this.modalRef.hide();
    this.toastrService.success('Mas olha q deu merda mesmo', 'Avisinho');
  }

  decline(): void {
    this.modalRef.hide();
  }

}
