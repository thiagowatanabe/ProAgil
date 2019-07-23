import { EventoService } from './../_services/evento.service';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { Evento } from '../_models/Evento';
import { BsModalRef, BsModalService } from 'ngx-bootstrap';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
})
export class EventosComponent implements OnInit {

  margem = 2;
  largura = 50;
  mostrarimagem = false;
  filtro = '';
  eventos: Evento[];
  eventosFiltrados: Evento[];
  modalRef: BsModalRef;
  _filtrolista = '';
  registerForm: FormGroup;

  constructor(private eventoService: EventoService
    , private modalService: BsModalService
    , private fb: FormBuilder) { }

    openModal(template: TemplateRef<any>){
      this.modalRef = this.modalService.show(template);
    }

    get filtroLista(): string {
      return this._filtrolista;
    }

    set filtroLista(value: string) {
      this._filtrolista = value;
      this.eventosFiltrados = this.filtroLista ? this.filtrarEventos(this._filtrolista) : this.eventos;
    }

    filtrarEventos(filtrarPor: string): Evento[] {
      filtrarPor = filtrarPor.toLowerCase();
      return this.eventos.filter(evento =>
        evento.tema.toLowerCase().indexOf(filtrarPor) !== -1
        );
      }

      validation() {
        this.registerForm = this.fb.group({
          tema: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
          local: ['', Validators.required],
          dataEvento: ['', Validators.required],
          qtdPessoas: ['', [Validators.required, Validators.max(120000)]],
          imagemUrl: ['', Validators.required],
          telefone: ['', Validators.required],
          email: ['', [Validators.required, Validators.email]],
        });
      }

      salvarAlteracao() {

      }

      ngOnInit() {
        this.validation();
        this.getEventos();
      }

      alternarImagem() {
        this.mostrarimagem = !this.mostrarimagem;
      }

      getEventos() {
        this.eventoService.getAllEvento().subscribe(
          (_eventos: Evento[]) => {
            this.eventosFiltrados = this.eventos = _eventos;
            console.log(_eventos);
          }, error => {
            console.log(error);
          });
        }
      }
