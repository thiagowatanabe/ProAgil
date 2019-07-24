import { EventoService } from './../_services/evento.service';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { Evento } from '../_models/Evento';
import { BsModalService } from 'ngx-bootstrap';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { defineLocale, BsLocaleService, ptBrLocale } from 'ngx-bootstrap';
defineLocale('pt-br', ptBrLocale);

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
  evento: Evento;
  eventosFiltrados: Evento[];
  filtrolista = '';
  registerForm: FormGroup;
  
  constructor(private eventoService: EventoService
    , private modalService: BsModalService
    , private fb: FormBuilder
    , private localService: BsLocaleService) {
      this.localService.use('pt-br');
    }
    
    openModal(template: any) {
      this.registerForm.reset();
      template.show();
    }
    
    get filtroLista(): string {
      return this.filtrolista;
    }
    
    set filtroLista(value: string) {
      this.filtrolista = value;
      this.eventosFiltrados = this.filtroLista ? this.filtrarEventos(this.filtrolista) : this.eventos;
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
          data: ['', Validators.required],
          qtdPessoas: ['', [Validators.required, Validators.max(120000)]],
          imagemUrl: ['', Validators.required],
          telefone: ['', Validators.required],
          email: ['', [Validators.required, Validators.email]]
        });
      }
      
      salvarAlteracao(template: any) {
        if (this.registerForm.valid) {
          this.evento = Object.assign({}, this.registerForm.value);
          this.eventoService.postEvento(this.evento).subscribe(
            (novoEvento: Evento) => {
              template.hide();
              this.getEventos();
            },
            error => {
              console.log(error);
            });
          }
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
            (eventos: Evento[]) => {
              this.eventosFiltrados = this.eventos = eventos;
              console.log(eventos);
            }, error => {
              console.log(error);
            });
          }
        }
        