import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { ApiService } from '../../services/api.service';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss'
})
export class DashboardComponent implements OnInit {
  // Loading states
  isLoading = true;
  error: string | null = null;

  // Statistics
  totalClients = 0;
  totalPolicies = 0;
  totalRevenue = 0;
  activeClaims = 0;

  // Real data from API
  clients: any[] = [];
  accounts: any[] = [];
  businessActivities: any[] = [];
  insuranceCompanies: any[] = [];

  // Recent Activities
  recentActivities: any[] = [];

  // Quick Actions
  quickActions = [
    { title: 'Add New Client', icon: 'fa-user-plus', route: '/clients' },
    { title: 'Manage Accounts', icon: 'fa-wallet', route: '/financial' },
    { title: 'Master Tables', icon: 'fa-table', route: '/master-table' },
    { title: 'View Reports', icon: 'fa-chart-bar', route: '/reports' }
  ];

  constructor(private apiService: ApiService) { }

  ngOnInit(): void {
    this.loadDashboardData();
  }

  async loadDashboardData(): Promise<void> {
    try {
      this.isLoading = true;
      this.error = null;

      // Load data in parallel
      const [clientsData, accountsData, businessActivitiesData, insuranceCompaniesData] = await Promise.all([
        this.apiService.getAllClients().toPromise(),
        this.apiService.getAllAccounts().toPromise(),
        this.apiService.getAllBusinessActivities().toPromise(),
        this.apiService.getAllInsuranceCompanies().toPromise()
      ]);

      this.clients = clientsData?.data || [];
      this.accounts = accountsData?.data || [];
      this.businessActivities = businessActivitiesData?.data || [];
      this.insuranceCompanies = insuranceCompaniesData?.data || [];

      // Calculate statistics
      this.totalClients = this.clients.length;
      this.totalPolicies = this.accounts.length;
      this.totalRevenue = this.accounts.reduce((sum: number, account: any) => sum + (account.balance || 0), 0);
      this.activeClaims = Math.floor(Math.random() * 50) + 10; // Mock data for now

      // Update recent activities based on real data
      this.updateRecentActivities();

    } catch (error) {
      console.error('Error loading dashboard data:', error);
      this.error = 'Failed to load dashboard data. Please try again.';
    } finally {
      this.isLoading = false;
    }
  }

  updateRecentActivities(): void {
    // Create activities based on real data
    const activities = [];
    
    if (this.clients.length > 0) {
      const latestClient = this.clients[0];
      activities.push({
        id: 1,
        type: 'client',
        message: `New client: ${latestClient.name || 'Unknown'}`,
        time: 'Just now'
      });
    }

    if (this.accounts.length > 0) {
      const latestAccount = this.accounts[0];
      activities.push({
        id: 2,
        type: 'account',
        message: `Account created: ${latestAccount.name || 'Unknown'}`,
        time: '5 minutes ago'
      });
    }

    if (this.insuranceCompanies.length > 0) {
      activities.push({
        id: 3,
        type: 'company',
        message: `Insurance company updated: ${this.insuranceCompanies[0].name || 'Unknown'}`,
        time: '1 hour ago'
      });
    }

    this.recentActivities = activities.slice(0, 5);
  }

  refreshData(): void {
    this.loadDashboardData();
  }

  formatCurrency(amount: number): string {
    return new Intl.NumberFormat('en-US', {
      style: 'currency',
      currency: 'USD'
    }).format(amount);
  }

  get currentDate(): Date {
    return new Date();
  }
}
