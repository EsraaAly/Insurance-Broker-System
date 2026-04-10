import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { ApiService } from '../../../../services/api.service';

@Component({
  selector: 'app-client-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './client-list.component.html',
  styleUrl: './client-list.component.scss'
})
export class ClientListComponent implements OnInit {
  clients: any[] = [];
  isLoading = false;
  error: string | null = null;
  searchTerm = '';
  currentPage = 1;
  pageSize = 10;
  totalClients = 0;

  constructor(
    private apiService: ApiService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadClients();
  }

  async loadClients(): Promise<void> {
    try {
      this.isLoading = true;
      this.error = null;
      
      const response = await this.apiService.getAllClients().toPromise();
      console.log('API Response:', response); // Debug log
      
      // Temporarily test empty state - remove this after testing
      this.clients = [];
      this.totalClients = 0;
      this.error = null;
      this.isLoading = false;
      return;
      
      // Handle the response structure properly
      if (response && response.succeeded) {
        this.clients = response.data || [];
        this.totalClients = this.clients.length;
        console.log('Clients loaded successfully:', this.clients.length); // Debug log
        // Clear any existing error since the call was successful
        this.error = null;
      } else {
        // Handle failed response
        this.clients = [];
        this.totalClients = 0;
        console.log('Response not successful:', response); // Debug log
        
        // Check for specific error messages
        if (response && response.errors && response.errors.length > 0) {
          this.error = response.errors.join(', ');
        } else if (response && response.message) {
          this.error = response.message;
        } else {
          this.error = 'Failed to load clients. Please try again.';
        }
      }
      
    } catch (error) {
      console.error('Error loading clients:', error);
      this.error = 'Failed to load clients. Please try again.';
      this.clients = [];
      this.totalClients = 0;
    } finally {
      this.isLoading = false;
    }
  }

  searchClients(): void {
    if (this.searchTerm.trim()) {
      this.clients = this.clients.filter(client => 
        client.name?.toLowerCase().includes(this.searchTerm.toLowerCase()) ||
        client.email?.toLowerCase().includes(this.searchTerm.toLowerCase()) ||
        client.phone?.includes(this.searchTerm)
      );
    } else {
      this.loadClients();
    }
  }

  editClient(clientId: number): void {
    this.router.navigate(['/clients/edit', clientId]);
  }

  async deleteClient(clientId: number): Promise<void> {
    if (confirm('Are you sure you want to delete this client?')) {
      try {
        this.isLoading = true;
        await this.apiService.deleteClient(clientId).toPromise();
        await this.loadClients(); // Reload the list
      } catch (error) {
        console.error('Error deleting client:', error);
        this.error = 'Failed to delete client. Please try again.';
      } finally {
        this.isLoading = false;
      }
    }
  }

  viewClientDetails(clientId: number): void {
    this.router.navigate(['/clients', clientId]);
  }

  addNewClient(): void {
    this.router.navigate(['/clients/add']);
  }

  refreshClients(): void {
    this.searchTerm = '';
    this.loadClients();
  }

  getStatusBadgeClass(status: string): string {
    switch (status?.toLowerCase()) {
      case 'active':
        return 'bg-success';
      case 'inactive':
        return 'bg-warning';
      case 'blocked':
        return 'bg-danger';
      default:
        return 'bg-secondary';
    }
  }

  get paginatedClients(): any[] {
    const startIndex = (this.currentPage - 1) * this.pageSize;
    const endIndex = startIndex + this.pageSize;
    return this.clients.slice(startIndex, endIndex);
  }

  changePage(page: number): void {
    this.currentPage = page;
  }

  get totalPages(): number {
    return Math.ceil(this.clients.length / this.pageSize);
  }

  getPages(): number[] {
    return Array.from({ length: this.totalPages }, (_, i) => i + 1);
  }
}
