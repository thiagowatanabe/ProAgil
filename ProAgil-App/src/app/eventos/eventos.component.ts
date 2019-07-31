import { EventoService } from './../_services/evento.service';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { Evento } from '../_models/Evento';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { defineLocale, BsLocaleService, ptBrLocale } from 'ngx-bootstrap';
import { ToastrService } from 'ngx-toastr';
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
  bodyDeletarEvento = '';
  registerForm: FormGroup;
  modoSalvar = '';
  titulo = 'Eventos';
  file: File;
  fileNameToUpdate: string;
  dataAtual: string;

  constructor(private eventoService: EventoService
    , private fb: FormBuilder
    , private localService: BsLocaleService
    , private toastr: ToastrService) {
      this.localService.use('pt-br');
    }
    
    excluirEvento(evento: Evento, template: any) {
      this.openModal(template);
      this.evento = evento;
      this.bodyDeletarEvento = `Tem certeza que deseja excluir o Evento: ${evento.tema}, Código: ${evento.id}`;
    }
    
    confirmeDelete(template: any) {
      this.eventoService.deleteEvento(this.evento.id).subscribe(
        () => {
          template.hide();
          this.toastr.success('Deletado com sucesso');
          this.getEventos();
        }, error => {
          this.toastr.error('Falha ao deletar');
          console.log(error);
        }
        );
      }
      
      editarEvento(evento: Evento, template: any) {
        this.openModal(template);
        this.modoSalvar = 'put';
        this.evento = Object.assign({}, evento);
        this.fileNameToUpdate = evento.imagemUrl.toString();
        this.evento.imagemUrl = '';
        this.registerForm.patchValue(this.evento);
      }
      
      novoEvento(template: any) {
        this.openModal(template);
        this.modoSalvar = 'post';
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
        
        uploadImagem() {
          if ( this.modoSalvar === 'post') {
            const nomeArquivo = this.evento.imagemUrl.split('\\', 3);
            this.evento.imagemUrl = nomeArquivo[2];
            this.eventoService.postUpload(this.file, nomeArquivo[2]).subscribe(
              () => {
                this.dataAtual = new Date().getMilliseconds().toString();
                this.getEventos();
              }
            );
          } else {
            this.evento.imagemUrl = this.fileNameToUpdate;
            this.eventoService.postUpload(this.file, this.fileNameToUpdate).subscribe(
              () => {
                this.dataAtual = new Date().getMilliseconds().toString();
                this.getEventos();
              }
            );
          }
        }
        
        onFileChange(event) {
          const reader = new FileReader();
          
          if ( event.target.files && event.target.files.length) {
            this.file = event.target.files;
          }
        }
        
        salvarAlteracao(template: any) {
          if (this.registerForm.valid) {
            if ( this.modoSalvar === 'post') {
              this.evento = Object.assign({}, this.registerForm.value);
              
              this.uploadImagem();
              
              this.eventoService.postEvento(this.evento).subscribe(
                () => {
                  template.hide();
                  this.toastr.success('Inserido com sucesso');
                  this.getEventos();
                },
                error => {
                  this.toastr.error('Falha ao inserir');
                  console.log(error);
                });
              } else {
                this.evento = Object.assign({id: this.evento.id}, this.registerForm.value);
                this.uploadImagem();
                this.eventoService.putEvento(this.evento.id, this.evento).subscribe(
                  () => {
                    template.hide();
                    this.toastr.success('Alterado com sucesso');
                    this.getEventos();
                  },
                  error => {
                    this.toastr.error('Falha ao alterar');
                    console.log(error);
                  });
                }
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
                }, error => {
                  this.toastr.error(`Falha ao carregar ${error}`);
                });
              }
            }