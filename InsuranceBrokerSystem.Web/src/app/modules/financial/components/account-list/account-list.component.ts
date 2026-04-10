import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { ApiService } from '../../../../services/api.service';

@Component({
  selector: 'app-account-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './account-list.component.html',
  styleUrl: './account-list.component.scss'
})
export class AccountListComponent implements OnInit {
  accounts: any[] = [];
  isLoading = false;
  error: string | null = null;
  searchTerm = '';
  currentPage = 1;
  pageSize = 10;
  totalAccounts = 0;

  constructor(
    private apiService: ApiService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadAccounts();
  }

  async loadAccounts(): Promise<void> {
    try {
      this.isLoading = true;
      this.error = null;
      
      const response = await this.apiService.getAllAccounts().toPromise();
      this.accounts = response?.data || [];
      this.totalAccounts = this.accounts.length;
      
    } catch (error) {
      console.error('Error loading accounts:', error);
      this.error = 'Failed to load accounts. Please try again.';
    } finally {
      this.isLoading = false;
    }
  }

  searchAccounts(): void {
    if (this.searchTerm.trim()) {
      this.accounts = this.accounts.filter(account => 
        account.name?.toLowerCase().includes(this.searchTerm.toLowerCase()) ||
        account.accountNumber?.toLowerCase().includes(this.searchTerm.toLowerCase()) ||
        account.accountType?.toLowerCase().includes(this.searchTerm.toLowerCase())
      );
    } else {
      this.loadAccounts();
    }
  }

  editAccount(accountId: number): void {
    this.router.navigate(['/financial/edit', accountId]);
  }

  async deleteAccount(accountId: number): Promise<void> {
    if (confirm('Are you sure you want to delete this account?')) {
      try {
        this.isLoading = true;
        await this.apiService.deleteAccount(accountId).toPromise();
        await this.loadAccounts(); // Reload the list
      } catch (error) {
        console.error('Error deleting account:', error);
        this.error = 'Failed to delete account. Please try again.';
      } finally {
        this.isLoading = false;
      }
    }
  }

  viewAccountDetails(accountId: number): void {
    this.router.navigate(['/financial', accountId]);
  }

  addNewAccount(): void {
    this.router.navigate(['/financial/add']);
  }

  refreshAccounts(): void {
    this.searchTerm = '';
    this.loadAccounts();
  }

  getStatusBadgeClass(status: string): string {
    switch (status?.toLowerCase()) {
      case 'active':
        return 'bg-success';
      case 'inactive':
        return 'bg-warning';
      case 'closed':
        return 'bg-danger';
      default:
        return 'bg-secondary';
    }
  }

  getAccountTypeBadgeClass(type: string): string {
    switch (type?.toLowerCase()) {
      case 'savings':
        return 'bg-primary';
      case 'checking':
        return 'bg-info';
      case 'investment':
        return 'bg-success';
      case 'loan':
        return 'bg-warning';
      default:
        return 'bg-secondary';
    }
  }

  formatCurrency(amount: number): string {
    return new Intl.NumberFormat('en-US', {
      style: 'currency',
      currency: 'USD'
    }).format(amount || 0);
  }

  get paginatedAccounts(): any[] {
    const startIndex = (this.currentPage - 1) * this.pageSize;
    const endIndex = startIndex + this.pageSize;
    return this.accounts.slice(startIndex, endIndex);
  }

  changePage(page: number): void {
    this.currentPage = page;
  }

  get totalPages(): number {
    return Math.ceil(this.accounts.length / this.pageSize);
  }

  getPages(): number[] {
    return Array.from({ length: this.totalPages }, (_, i) => i + 1);
  }

  get activeAccountsCount(): number {
    return this.accounts.filter(acc => acc.status === 'Active').length;
  }

  get inactiveAccountsCount(): number {
    return this.accounts.filter(acc => acc.status === 'Inactive').length;
  }

  get closedAccountsCount(): number {
    return this.accounts.filter(acc => acc.status === 'Closed').length;
  }

  get totalBalance(): number {
    return this.accounts.reduce((sum, acc) => sum + (acc.balance || 0), 0);
  }
}
